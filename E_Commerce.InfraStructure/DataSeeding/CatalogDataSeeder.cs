using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Common;
using E_Commerce.Domain.Entities.Order;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace E_Commerce.InfraStructure.DataSeeding
{//ana al h5ly al Clr h3ml create ll Catalog data seeder // azay ?? h3ml reg service bs f al infra structre 34an a7na faslen
    //w ant btt3aml m3 al logger ab2a 2olo Logg of eh !
    class CatalogDataSeeder(StoreDbContext dbContext,ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        //Check Primary Ctor
        //b3d ma t5ls lazm t3ml Reg ll method de  // a7na bn3ml seeding 2bl al app ma yt3aml m3a awel req // 2bl ma y3ml configue ll midleware


        //aB2A a3ml check lma al net yrg3 34an msh m5zn fe al data base w by2ol an fe data <<*******************************
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try {
                   //1  - Check for Pending Migration
                   //Return Collection mn al pending migration string
                var PendingMigration=await dbContext.Database.GetPendingMigrationsAsync(ct);

                if (PendingMigration.Any())
                {
                   await dbContext.Database.MigrateAsync(ct);
                    //Done migrated
                }


                //Seeding step
                //Default path (Bin==>Debug folder in Project)

                //1- Path C:\Users\COMPUMARTS\source\repos\E_Commerce\E_Commerce.Api\bin\Debug\net8.0\DataSeed\* l7d hna Sabt ant 3ayz da dynamic *   Products.json

                //SeedRoot        C:\Users\COMPUMARTS\source\repos\E_Commerce\E_Commerce.Api\bin\Debug\net8.0\     
                var seedRoot = Path.Combine(AppContext.BaseDirectory , "DataSeed");

                // lazm a3ml al Fk al awel 
              await  SeedIfEmptyAsync<ProductBrand,int>(seedRoot, "brands.json",ct);
              await  SeedIfEmptyAsync<ProductType,int>(seedRoot, "types.json",ct);
              await  SeedIfEmptyAsync<Product,int>(seedRoot, "products.json",ct);
              await  SeedIfEmptyAsync<DeliveryMethod,int>(seedRoot, "delivery .json", ct);

                //Low far2 m3ak kam row at3mlo insert ast2blo f result
            int result=  await  dbContext.SaveChangesAsync(ct);
                if (result>0)
                {
                    logger.LogInformation($"{result} Row Added");
                }
                else
                {
                    logger.LogInformation("DataBase already Seeded"); 
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        //RootPath  = seed root     ,,   T = Products , Type , Brand 
        private async Task SeedIfEmptyAsync<T,Tkey>(string rootPath,string fileName , CancellationToken ct) where T : BaseEntity<Tkey>
        
        {
           
            //check low al T feha data wala la2 al T proucts , type , brand
            var checkResult = await dbContext.Set<T>().AnyAsync(ct);
            if ( checkResult)
            {
                logger.LogInformation("Table has Data");
                return;  
            }

            var filePath = Path.Combine(rootPath, fileName);

            //Check if filePath exists or not y3ni al file mwgod f3ln wala la2
            if (!File.Exists(filePath))
            {
                logger.LogInformation($"File{fileName} is not Exists");
                return;
                
            }

            //Using 34an stream w 3ayz 22flo b3d ma y5ls
           using var fileStream = File.OpenRead(filePath);


            //de 34an y3ml ignore ll case sens w hwa bykarn ben al module w al json file
            var options = new JsonSerializerOptions() {

                PropertyNameCaseInsensitive = true
            };
            //ht2ra list of T mn al data al bt3mlha seed
           //hyrg3lk list of product list of brand list of type
           //al rag3lk f3lan al item al html3ha insert
           //lazm tt2kf an al prop nafs al asamy al module al 3ndk b al json file
          var items= await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options,ct);

            //if msh bta5od null         de hy4yl kolo w y7ot false
            if (items?.Any()??false)
            {
                dbContext.Set<T>().AddRange(items);
                //Hna h3ml add local bs 
            }
        }
    }







    
}
