using AutoMapper;
using Awushi.Application.DTO.Product;
using Awushi.Application.InputModels;
using Awushi.Application.OutputModels;
using Awushi.Application.Services.Interface;
using Awushi.Domain.Contracts;
using Awushi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IPaginationService<ProductDto,Product> _paginationService;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository, IPaginationService<ProductDto, Product> paginationService, IMapper mapper)
        {
            _repository = repository;
            _paginationService = paginationService;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var createdEntity = await _repository.CreateAsync(product);
            var entity = _mapper.Map<ProductDto>(createdEntity);
            return entity;

        }

        public async Task DeleteAsync(int id)
        {
            var product =await _repository.GetByIdAsync(x=>x.Id == id); 
            await _repository.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var product = await _repository.GetAllProductAsync();
            return _mapper.Map<List<ProductDto>>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllByFilterAsync(int? categoryId, int? brandId, double? price)
        {
            var data = await _repository.GetAllProductAsync();
            IEnumerable<Product> query = data;


            if (categoryId>0)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }
            if (brandId>0)
            {
                query = query.Where(x=>x.BrandId == brandId);
            }
            if (price>0)
            {
                query = query.Where(x => x.Price == price);
            }

            var result = _mapper.Map<List<ProductDto>>(query);
            return result;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product =await _repository.GetDetailsAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PaginationVM<ProductDto>> GetPagination(PaginationInputModel pagination)
        {
            var source =await _repository.GetAllProductAsync();
            var result = _paginationService.GetPagination(source, pagination);
            return result;
        }

        public async Task UpdateAsync(UpdateProductDto updateProductDto)
        {
            var product =  _mapper.Map<Product>(updateProductDto);
            await _repository.UpdateAsync(product);
        }
    }
}
