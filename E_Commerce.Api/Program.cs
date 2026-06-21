
using System.Threading.Tasks;
using E_Commerce.Api.Extentions;
using E_Commerce.Application.AppServicesRegistration;
using E_Commerce.Domain.Contracts;
using E_Commerce.InfraStructure;

namespace E_Commerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            
            builder.Services.AddControllers();


            #region Layer Services Reg
            //*********************

            builder.Services.AddInfraServices(builder.Configuration);

            builder.Services.AddApplicationServices(); 
            #endregion
            //***********************


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //WebApp
            var app = builder.Build();

            #region DataSeed reg 
            //Seeding reg
            //bs 34an al project hykbr f hnfslha f extention Method  ,, ht3aml m3aha 3la anha midleware
            //***********************


            //  using var scope = app.Services.CreateScope();
            //  var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            //await  seeder.SeedDataAsync();

            //De mn al extention method lma deftha w rg3tha
          await app.seedAndMigrateDataAsync();


    #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
