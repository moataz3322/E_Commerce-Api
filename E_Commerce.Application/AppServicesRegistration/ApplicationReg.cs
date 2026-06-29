using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Profiles;
using E_Commerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Application.AppServicesRegistration
{
   public static class ApplicationReg
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            #region Old way to reg mapper
            //hna b3ml add profile
            //de bb3tlo list of prifles w da awel profile 3mltlo create
            //services.AddAutoMapper(c => c.AddProfiles(new[] { new ProductProfile()}));
            //return services; 
            #endregion

            //de tary2a t3ml beha al getExucuting 3ltol 
            //services.AddAutoMapper(c => { }, Assembly.GetExecutingAssembly());

            //Fe tare2a tanya ab3t ay class mn al Application w hwa ygeb al Assembly



            //kda ay class hykon bywrs Profile hydefo 3ndo k Profile ========>Moheeeeeeeeeeeem

            services.AddAutoMapper(c => { }, typeof(ApplicationReg).Assembly);
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            return services;
        }
    }
}
