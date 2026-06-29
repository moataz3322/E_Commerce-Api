using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Common;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.InfraStructure.Data;
using E_Commerce.InfraStructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.InfraStructure.Repos
{
    class GenaricRepo<TEntity, Tkey>(StoreDbContext dbContext) : IGenaricRepo<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);

        public async Task<IReadOnlyList<TEntity>?> GetAllAsync(CancellationToken ct = default) =>await dbContext.Set<TEntity>().ToListAsync(ct);






        public async Task<IReadOnlyList<TEntity>?> GetAllAsync(ISpecifications<TEntity, Tkey> Spec, CancellationToken ct = default)
        {
            #region da al kan 2adem w 3mlnalo helper method
            //almfrod hna akon al Query
            //1-entryPoint _dbcont.set<T> 
            // al hyt8yer include , where de al htkon fe al Spec

            //IQueryable<TEntity> query = dbContext.Set<TEntity>();

            //if (Spec != null)
            //{
            //    if (Spec.IncludeExpression.Any())
            //    {//ant 3aml list mn al includes f lazm For
            //        foreach (var expression in Spec.IncludeExpression)

            //        //ant btdef 3ala al adem 34an da al htrg3o f al a5er
            //        {
            //            query = query.Include(expression);



            //        }

            //    }      return await query.ToListAsync(ct);
            //} 
            #endregion

            //Men al 7ddlo al Tkey ????????????????
            var query = Evaluator.CreateQuery(dbContext.Set<TEntity>(), Spec);

            return await query.ToListAsync(ct);

        }






        public async Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default)=>await dbContext.Set<TEntity>().FindAsync(id,ct);

        public Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> spec, CancellationToken ct = default)
        {
            //de btrg3 al iqueryble
           var query= Evaluator.CreateQuery(dbContext.Set<TEntity>(), spec);

            //first or defult 34an ana mot2kd ano elment wa7ed bs
            return query.FirstOrDefaultAsync(ct);
        }

        public async Task<int> GetCount(ISpecifications<TEntity, Tkey> Spec, CancellationToken ct = default)
        {//Here will check for Ceritria Only Thats why im going to use same evaluator but with new Class Specification

            //mtnsa4 al bykwnlk al query aslun howa al evaluator
            return await Evaluator.CreateQuery(dbContext.Set<TEntity>(), Spec).CountAsync();
        }

        public void Remove(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)=>dbContext.Set<TEntity>().Update(entity);



         
    }
}
