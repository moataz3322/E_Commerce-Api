using E_Commerce.Domain.Contracts;

namespace E_Commerce.Api.Extentions
{
    public static class WebAppExtenstion
    {

        //de al tare2a low 3ayz t3ml reg l ay 7aga f al Program bs tfslha f extention Method 3n tare2
        // ank t3ml object mn al webApp w tdefha feh
        public static async Task<WebApplication> seedAndMigrateDataAsync(this WebApplication app) {

            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            var identitySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");
            var identitySeederTest = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("IdentityTest");
            await  seeder.SeedDataAsync();
            await  identitySeeder.SeedDataAsync();
            await identitySeederTest.SeedDataAsync();
            //Return ll app y3ni ba5od al object adef feh 7aga w arg3ha tany
            return app;

        }
    }
}
