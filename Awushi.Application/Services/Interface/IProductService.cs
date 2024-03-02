using Awushi.Application.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.Services.Interface
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync (int id);
        Task<IEnumerable<ProductDto>> GetAllAsync ();
        Task<IEnumerable<ProductDto>> GetAllByFilterAsync (int? categoryId, int? brandId);
        Task<ProductDto> CreateAsync (CreateProductDto createProductDto);
        Task UpdateAsync (UpdateProductDto updateProductDto);
        Task DeleteAsync (int id);
    }
}
