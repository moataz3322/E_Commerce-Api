using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Contracts;
using E_Commerce.InfraStructure.Identity.Data;
using E_Commerce.InfraStructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Commerce.InfraStructure.DataSeeding
{
    class IdentitySeederTest : IDataSeeder
    {
        private readonly StoreIdentityDbContext dbContext;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<IdentitySeederTest> logger;

        public IdentitySeederTest(StoreIdentityDbContext dbContext,RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager,ILogger<IdentitySeederTest> logger)
        {
            this.dbContext = dbContext;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var PendingMigration = dbContext.Database.GetPendingMigrations();

                if (PendingMigration.Any())
                {
                    await dbContext.Database.MigrateAsync();
                }


                if (!await roleManager.Roles.AnyAsync())

                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

                }





                var UserCheck = await userManager.Users.AnyAsync();

                if (!UserCheck)
                {
                    var admin = new ApplicationUser()
                    {
                        DisplayName = "MoatazTest",
                        PhoneNumber = "0108300305",
                        Email = "MoatazTest@gmail.com",
                        UserName="Moatazzzzzzzzzz"

                    };

                    var Result = await userManager.CreateAsync(admin, "P@ss0word");

                    if (Result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "SuperAdmin");

                    }
                    else
                    {
                        var errors = string.Join(';', Result.Errors.Select(x => x.Description));
                        logger.LogError($"Canot Seed Defult Admin {errors}");
                    }

                }

            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Identity Seed Failed ");
            }







        }
    }
}
