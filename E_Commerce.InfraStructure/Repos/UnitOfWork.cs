using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Common;
using E_Commerce.InfraStructure.Data;

namespace E_Commerce.InfraStructure.Repos
{
    class UnitOfWork(StoreDbContext dbcontext) : IUnitOfWork
    {
        //inline init 34an mmkn ykon b null
        private readonly Dictionary<string, object> Repos = [];
        public IGenaricRepo<Tentity, Tkey> GetRepo<Tentity, Tkey>() where Tentity : BaseEntity<Tkey>
        {

            var typeName = typeof(Tentity).Name;
            //1- check al repo at3ml abl kda wla la2 
            if (Repos.TryGetValue(typeName, out object? value))
            {
                //casting

                ////// vipppppppppppppppppppppppppppppppppppppppppppppppp
                //w hna ant 3aml creat l object IGenaric repo hwa da al rabet al IunitOfWork b al Genaric Repo
                return (IGenaricRepo<Tentity, Tkey>)value;
            }
            else 
            {
                //5lek fakr ank btt3aml m3 al repo  w da new object
                var repo = new GenaricRepo<Tentity, Tkey>(dbcontext);
                //Add To Dic
                Repos[typeName] = repo;
                
                return repo;

            
            
            }


        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => dbcontext.SaveChangesAsync(ct);
    }
}
