using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OMX.Data;
using OMX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftUniClone.Common
{
    public static class ApplicationBuilder
    {
        private const string DefaultAdminPassword = "admin123";

        private static IdentityRole[] roles =
            {
                 new IdentityRole("Administrator"),
                 new IdentityRole("Moderator")
            };




        public static async void SeedDatabase(this IApplicationBuilder app)

        {

            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var context = scope.ServiceProvider.GetRequiredService<OmxDbContext>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }

                }
                var user = await userManager.FindByNameAsync("atrusinov@gmail.com");
                if (user != null)
                {
                    await userManager.CreateAsync(new User()
                    {
                      Email = "atrusinov@gmail.com",
                      FullName = "Atanas Rusinov"
                    }, DefaultAdminPassword);
                    await userManager.AddToRoleAsync(user, roles[0].Name);
                }

                if (!context.Features.Any())
                {
                    Feature[] features =
                    {
                          new Feature() {  Name = "Covered Parking" },
                          new Feature() {  Name = "Pool Access" },
                          new Feature() {  Name = "Pool Bar" },
                          new Feature() {  Name = "Garden Access" },
                          new Feature() {  Name = "Rooftop Terrace" },
                          new Feature() {  Name = "Private Pool" },
                          new Feature() {  Name = "Small Pets Only" },
                          new Feature() {  Name = "Free Wifi" },

                     };

                    context.Features.AddRange(features);
                    context.SaveChanges();
                }
                if (!context.Addresses.Any())
                {
                    Address[] addresses =
                    {
                          new Address() {  City = "Sofia" },
                          new Address() {  City = "Plovdiv" },
                          new Address() {  City = "Varna" },
                          new Address() {  City = "Burgas" },
                          new Address() {  City = "Stara Zagora" },
                          new Address() {  City = "Yambol" },
                          new Address() {  City = "Ruse" },
                          new Address() {  City = "Pleven" },

                     };

                    context.Addresses.AddRange(addresses);
                    context.SaveChanges();
                }


            }
        }
    }
}
