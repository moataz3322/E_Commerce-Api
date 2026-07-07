using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using E_Commerce.InfraStructure.Data;
using E_Commerce.InfraStructure.DataSeeding;
using E_Commerce.InfraStructure.Identity.Data;
using E_Commerce.InfraStructure.Identity.Entities;
using E_Commerce.InfraStructure.Identity.Services;
using E_Commerce.InfraStructure.NewIdentityTest.Data;
using E_Commerce.InfraStructure.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace E_Commerce.InfraStructure
{
   public static class InfraStructureRegisteration 
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services , IConfiguration config) {
            services.AddDbContext<StoreDbContext>(options => {
            
            
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                
            });

            //Identity Db
            services.AddDbContext<StoreIdentityDbContext>(options => { 
            
            
            options.UseSqlServer(config.GetConnectionString("IdentityConnection"));
            });


         
            services.AddDbContext<IdentetyTestDbContext>(options => { 
            
            
            options.UseSqlServer(config.GetConnectionString("IdentityConnectionTest"));
            });


         
            //Hna hst5d al keyed Service 34an ana hst5dm al seed f kaza 7ta f al ashl a3rfo eh al key bta3ha
            //34an hy3kon 3andy aktr mn imp lehom
            //services.AddScoped<IDataSeeder, CatalogDataSeeder>();

            services.AddKeyedScoped<IDataSeeder,CatalogDataSeeder>("Catalog");
            services.AddKeyedScoped<IDataSeeder,IdentityDataSeeder>("Identity");
            services.AddKeyedScoped<IDataSeeder,IdentitySeederTest>("IdentityTest");
           
            //  services.AddKeyedScoped<IDataSeeder, IdedntyDataSeeder>("Identitiy");
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>(
                configure => { return ConnectionMultiplexer.Connect(config.GetConnectionString("RedisConnection")!);
            });

         services.AddScoped<IBasketRepo , BasketRepo>();
         services.AddScoped<IIdentityService , IdentityService>();
            //tEST FOR Auth
         services.AddScoped<IIdentityServiceTest , IdentityServiceTest>();

            services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();

            //Ht3ml reg ll genaric w al UOW fen ???!

            return services;
        }
    }
}
