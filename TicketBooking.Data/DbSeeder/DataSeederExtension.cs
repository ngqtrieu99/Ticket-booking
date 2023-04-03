using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TicketBooking.Data.DbSeeder
{
    public static class DataSeederExtension
    {
        public static IApplicationBuilder InitSeeder(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializer = serviceScope.ServiceProvider.GetServices<IDataSeeder>();

            foreach (var init in initializer)
            {
                init.InitDataBase();
            }

            return app;
        }
    }
}
