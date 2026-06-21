using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Contracts
{
   public interface IUnitOfWork
    {

        Task<int> SaveChangesAsync(CancellationToken ct=default);

        IGenaricRepo<Tentity, Tkey> GetRepo<Tentity, Tkey>() where Tentity : BaseEntity<Tkey>;

    }
}
