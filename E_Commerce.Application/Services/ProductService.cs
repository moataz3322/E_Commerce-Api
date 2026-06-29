using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Dtos.Products;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;

namespace E_Commerce.Application.Services
{//da al byst5dm al Specfication
    class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
         var brands= await  _unitOfWork.GetRepo<ProductBrand, int>().GetAllAsync(ct);
            //3ayz Mapping 34an trg3o Dto LATEST STABLE VERSION 15


            var mapping = _mapper.Map<IReadOnlyList<BrandDto>>(brands);



            //hna ana rg3t al data low Success
            // mmkn mktb4 Ok 34an da al defult bs a3ml Operator OverLoading
            return Result<IReadOnlyList<BrandDto>>.Ok(mapping);




        }

        public async Task<Result<paginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default)
        {
            //ana 3mlt al calss al 4ayl al includes 34an ab3tha ll method bta3t al get All Async low m3aya include
            var spec = new ProductWithTypeAndBrandSpec(queryParams);

            var products=await _unitOfWork.GetRepo<Product,int>().GetAllAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            //Al query params hna msh hta5od 7aga 8er al certiria ant al 3aml kda hnak
            var countSpec = new PaginationSpecifications(queryParams);
            var dataCount =await _unitOfWork.GetRepo<Product, int>().GetCount(countSpec);
            var paginated = new paginatedResult<ProductDto>(queryParams.PageSize,queryParams.PageIndex, dataCount, data);
            return Result<paginatedResult<ProductDto>>.Ok(paginated);

        }

      

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var types=await _unitOfWork.GetRepo<ProductType,int>().GetAllAsync(ct);
            var result = _mapper.Map < IReadOnlyList<TypeDto>>(types);
            return Result<IReadOnlyList<TypeDto>>.Ok(result);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new ProductWithTypeAndBrandSpec(id);
            var product =await _unitOfWork.GetRepo<Product, int>().GetByIdAsync(spec);
            if (product == null)
            {
                return Result<ProductDto>.Fail(Error.NotFound($"Product with id {id} Is Not Found"));
            }
            else { 
            return Result<ProductDto>.Ok(_mapper.Map<ProductDto>(product));
            }
        }
    }
}
