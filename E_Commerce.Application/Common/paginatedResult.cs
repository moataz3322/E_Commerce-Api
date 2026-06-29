using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
   public sealed class  paginatedResult<TEntity>
    {
        public paginatedResult(int pageSize, int pageIndex, int count, IReadOnlyList<TEntity> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;
            Data = data;
        }

        public int PageSize { get;  }
        public int PageIndex { get;  }
        public int Count { get;  }
        public IReadOnlyList<TEntity> Data { get; }
    }
}
