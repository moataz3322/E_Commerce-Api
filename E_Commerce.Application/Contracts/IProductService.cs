using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce.Application.Common;
using E_Commerce.Application.Dtos.Products;

namespace E_Commerce.Application.Contracts
{
  public  interface IProductService
    {

        //Hna ana 3ayz arg3 Result Pattren ana m7tag a3rf bs succes wla la2 w eh al error
        //
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(CancellationToken ct=default);
        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct=default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct=default);


        Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default);








    }
}
