using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.InfraStructure
{
   public static class InfraStructureRegisteration 
    {
        public static IServiceCollection AddInfraServices(this IServiceCollection services , IConfiguration config) {
            services.AddDbContext<StoreDbContext>(options => {
            
            
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            });
            return services;
        }
    }
}
