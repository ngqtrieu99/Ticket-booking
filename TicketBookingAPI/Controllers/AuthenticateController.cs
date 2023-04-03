using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using TicketBooking.Service.Services.AuthenticateService;
using TicketBooking.Service.Models;
using TicketBooking.Model.Models;

namespace TicketBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService accountService;

        public AuthenticateController(IAuthenticateService account)
        {
            accountService = account;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUp model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.SignUp(model);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Sign up failed");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignIn model)
        {
            var result = await accountService.SignIn(model);
            if (result != null)
            {
                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpPost("renew-token")]
        public async Task<IActionResult> RenewToken(TokenResponse model)
        {
            if (ModelState.IsValid)
            {
                var result = await accountService.RenewToken(model);
                if (result != null)
                {
                    return Ok(result);
                }
                return Unauthorized();
            }
            else
                return BadRequest(ModelState);
        }
    }
}