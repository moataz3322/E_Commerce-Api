using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Contracts
{
   public interface IGenaricRepo<TEntity,Tkey>where TEntity : BaseEntity<Tkey>
    {

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct=default);

        Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken ct = default);


    }
}
