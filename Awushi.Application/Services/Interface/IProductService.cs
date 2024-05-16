using Awushi.Application.DTO.Product;
using Awushi.Application.InputModels;
using Awushi.Application.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.Services.Interface
{
    public interface IProductService
    {
        Task<PaginationVM<ProductDto>> GetPagination(PaginationInputModel pagination);
        Task<ProductDto> GetByIdAsync (int id);
        Task<IEnumerable<ProductDto>> GetAllAsync ();
        Task<IEnumerable<ProductDto>> GetAllByFilterAsync (int? categoryId, int? brandId, double? price);
        Task<ProductDto> CreateAsync (CreateProductDto createProductDto, string imageUrl);
        Task UpdateAsync (UpdateProductDto updateProductDto);
        Task DeleteAsync (int id);
    }
}
