using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using E_Commerce.Domain.Contracts;
using E_Commerce.InfraStructure.Identity.Data;
using E_Commerce.InfraStructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Commerce.InfraStructure.DataSeeding
{
    internal class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext _dbContext;
        private readonly UserManager<ApplicationUser> userManger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger logger;

        public IdentityDataSeeder(StoreIdentityDbContext dbContext
            , UserManager<ApplicationUser> userManger, RoleManager<IdentityRole> roleManager, ILogger<IdentityDataSeeder> logger)
        {
            _dbContext = dbContext;
            this.userManger = userManger;
            this.roleManager = roleManager;
            this.logger = logger;
        }
        public async Task SeedDataAsync(CancellationToken ct = default)
        {

            try
            {
                var PendingMigration = await _dbContext.Database.GetPendingMigrationsAsync();

                if (PendingMigration.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }


                //Check for Roles And Useres

                if (!await roleManager.Roles.AnyAsync())
                {//Creating User
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }



                if (!await userManger.Users.AnyAsync())
                {
                    var admin = new ApplicationUser()
                    {
                        DisplayName = "Moataz Mohamed",
                        Email = "Moataz@gmail.com"
                        ,
                        UserName = "MoatazMohamed"
                        ,
                        PhoneNumber = "0108300305"

                    };

                    var createResult = await userManger.CreateAsync(admin, "P@ssw0rd");

                    if (createResult.Succeeded)
                    {
                        await userManger.AddToRoleAsync(admin, "SuperAdmin");
                    }
                    else
                    {
                        // var Errors= createResult.Errors.Select(e => e.Description).ToList();
                        var Errors = string.Join(';', createResult.Errors.Select(e => e.Description));
                        logger.LogError($"Canot Seed Defult Admin {Errors}");

                    }




                }

            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Identity Data Seeding Failed");
            }




        }
    }
}
