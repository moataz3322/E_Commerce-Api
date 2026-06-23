using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Common;

namespace E_Commerce.Application.Specifications
{//Dal al asas kolo hym4y 3leha
    internal abstract class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        //ht3ml set azay ? ana h3ml helperMethod de al htdef
      
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

        //m7d4 8ery 3ndo ooption ano y3ml set
        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        //dol option    
            public Expression<Func<TEntity, object>> OrderBy { get; private set; }

            public Expression<Func<TEntity, object>> OrderByDesc { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected BaseSpecifications(Expression<Func<TEntity, bool>> condtion)
        {
            Criteria = condtion;
        }



        //ana 3ayz agbro f 3mltha fe Ctor
        //protected void AddCriteria(Expression<Func<TEntity, bool>> condetion) { 

        //Criteria   = condetion;

        //}

        //de bt3ml add includes fe al List only
        protected void AddIncludeExpression(Expression<Func<TEntity, object>> inculde) 
        {
        IncludeExpression.Add(inculde);
        
        
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp) 
        {
       OrderBy=orderByExp;
        
        }  protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByExp) 
        {
       OrderByDesc=orderByExp;
        
        }
        protected void ApplyPagination(int pageSize, int pageIndex)
        {
            IsPaginated=true;
           Take=pageSize;

            //ant mslan 3ayz page 3 5las al Skip hyb2a 2 
           Skip=(pageIndex - 1)*pageSize;
        }
    }
}
