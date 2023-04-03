using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TicketBooking.Data.DataModel;
using TicketBooking.Data.Infrastructure;
using TicketBooking.Data.Repository;
using TicketBooking.Model.Models;
using TicketBooking.Model.DataModel;
using TicketBooking.Service.Models;

namespace TicketBooking.Service.Services.AuthenticateService
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IRefreshTokenRepository refreshTokenRepo;
        private readonly IUnitOfWork unit;

        public AuthenticateService(UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , SignInManager<ApplicationUser> signInManager
            , IConfiguration configuration
            , IRefreshTokenRepository refreshToken,
            IUnitOfWork unitOfWork
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            refreshTokenRepo = refreshToken;
            unit = unitOfWork;
        }

        //Sign in
        public async Task<Response> SignIn(SignIn model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                return new Response
                {
                    Status = false,
                    Message = "Invalid username/password",
                    Data = null,
                };
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            var token = await GenerateToken(user);
            return new Response
            {
                Status = true,
                Message = "Authenticate Success",
                Data = token
            };
        }

        public async Task<TokenResponse> GenerateToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                JwtId = token.Id,
                UserId = user.Id,
                Token = refreshToken,
                IsUsed = false,
                IsReVoke = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };
            await refreshTokenRepo.AddToken(refreshTokenEntity);
            await unit.CompletedAsync();
            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        //Sign up for nomal user
        public async Task<Response> SignUp(SignUp model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            //Check sign up information
            if (userExists != null)
                return new Response { Status = false, Message = "User already exists!" };
            if (model.Password != model.ConfirmPassword)
                return new Response { Status = false, Message = "Confirm password is not same the Password!" };

            var user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            await CreateRoleAsync();
            var result = await userManager.CreateAsync(user, model.Password);
            if (model.IsAdmin == true)
            {
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            else
                await userManager.AddToRoleAsync(user, UserRoles.User);
            //Check sign up succeed or not
            if (!result.Succeeded)
                return new Response { Status = false, Message = "User creation failed! Please check user details and try again." };

            return new Response { Status = true, Message = "User created successfully!" };
        }

        //Sign up for admin
        private async Task CreateRoleAsync()
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        public async Task<Response> RenewToken(TokenResponse response)
        {
            var accessToken = response.AccessToken;
            var refreshToken = response.RefreshToken;
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(configuration["JWT:Secret"]);
            var tokenValidatePagram = new TokenValidationParameters
            {
                //tu cap token
                ValidateIssuer = false,
                ValidateAudience = false,

                //ky vao token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false //khong kiem tra token het han
            };
            try
            {
                //check1: access token valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(accessToken, tokenValidatePagram, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals
                        (SecurityAlgorithms.HmacSha512Signature, StringComparison.CurrentCultureIgnoreCase);
                    if (!result)
                    {
                        return new Response
                        {
                            Status = false,
                            Message = "invalid token"
                        };
                    }
                }
                //check access token expire
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return new Response
                    {
                        Status = false,
                        Message = "Access token has not yes expire"
                    };
                }
                //check 3: check refresh token exist in DB
                var storedToken = refreshTokenRepo.FindRefreshToken(refreshToken);
                if (storedToken == null)
                {
                    return new Response
                    {
                        Status = false,
                        Message = "refresh token does not exist"
                    };
                }
                //check 4: check if refresh token is used
                if (storedToken.IsUsed)
                {
                    return new Response
                    {
                        Status = false,
                        Message = "refresh token has been used"
                    };
                }
                if (storedToken.IsReVoke)
                {
                    return new Response
                    {
                        Status = false,
                        Message = "refresh token has been revoked"
                    };
                }
                //check 5: AccessToken id = JwtId in RefreshToken
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new Response
                    {
                        Status = false,
                        Message = "Token does'n match"
                    };
                }
                //Update token is  used
                storedToken.IsReVoke = true;
                storedToken.IsUsed = true;
                refreshTokenRepo.UpdateToken(storedToken);
                await unit.CompletedAsync();
                // create new token 
                var user = await userManager.FindByIdAsync(storedToken.UserId);
                var token = await GenerateToken(user);
                return new Response
                {
                    Status = true,
                    Message = "Renew token Success",
                    Data = token
                };
            }
            catch
            {
                return new Response
                {
                    Status = false,
                    Message = "SomeThing went wrong"
                };
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();
            return dateTimeInterval;
        }
    }
}