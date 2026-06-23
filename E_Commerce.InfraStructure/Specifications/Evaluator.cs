using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.InfraStructure.Specifications
{  //Hna al helper methods al hst5dmha fe al repos
    internal static class Evaluator
    {

        //Spec-> Query gahza
        //1- m7tag al Entry point (Dbcontext.set<T>) param

        //2- b3d kda tb3tly al Componanet bta3t al spec m3ak include wla la2 m3ak where wala la2 !! object
      
        public static  IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> enteryPoint , ISpecifications<TEntity,Tkey> spec) where TEntity : BaseEntity<Tkey>
        
        {//DbContext w de sabta
            var query = enteryPoint;

            //deft al where 2abl w da hyfr2 m3aya

            if (spec.Criteria!=null)
            {
                query = query.Where(spec.Criteria);

            }

            if (spec.IncludeExpression.Any())
            {
                query = spec.IncludeExpression.Aggregate(query,(current,nextExpress)=>current.Include(nextExpress));

            }
            //OrderBy Evaluator
            if (spec.OrderBy != null)
            {
               query= query.OrderBy(spec.OrderBy);
               
            }
            else if (spec.OrderByDesc != null)
            {
                query = query.OrderBy(spec.OrderByDesc);
            }
            if (spec.IsPaginated)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
                
            }
            //m3mlt4 hna query.tolistAsync()  34an ana h8yr al op al htnfzha
            return query;



            #region Dal al kona bnghzo w hwa kan fe al Repo
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

            //    }
            //}

            //return await query.ToListAsync(ct);

            #endregion

        }









    }
}
