using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Common;
using E_Commerce.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.InfraStructure.Repos
{
    class GenaricRepo<TEntity, Tkey>(StoreDbContext dbContext) : IGenaricRepo<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);

        public async Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken ct = default) =>await dbContext.Set<TEntity>().ToListAsync(ct);

        public async Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default)=>await dbContext.Set<TEntity>().FindAsync(id,ct);

        
        public void Remove(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)=>dbContext.Set<TEntity>().Update(entity);
    }
}
