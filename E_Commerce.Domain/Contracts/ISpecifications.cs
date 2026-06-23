using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Domain.Contracts
{//3mlo nfs al where 34an dol bs al htt3aml m3ahom
   public interface ISpecifications<TEntity,Tkey>where TEntity : BaseEntity<Tkey>
    {
        //icollection 34an a3rf add w remove w de al 4ayla kol al includes al htdefha f List
        //Includes
        ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; }

        //criteria = condtion
        Expression<Func<TEntity,bool>> Criteria { get; }
        Expression<Func<TEntity,object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDesc { get; }

        //Pagination
        public int Take { get;}
        public int Skip { get; }

        public bool IsPaginated{ get;  }







    }
}
