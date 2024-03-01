﻿using AutoMapper;
using Awushi.Application.DTO.Product;
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
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository,  IMapper mapper)
        {
            _repository = repository;
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
            var product = await _repository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(product);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product =await _repository.GetByIdAsync(x=>x.Id == id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task UpdateAsync(UpdateProductDto updateProductDto)
        {
            var product =  _mapper.Map<Product>(updateProductDto);
            await _repository.UpdateAsync(product);
        }
    }
}
