﻿using AutoMapper;
using Awushi.Application.DTO.Brand;
using Awushi.Application.Exceptions;
using Awushi.Application.Services.Interface;
using Awushi.Domain.Contracts;
using Awushi.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application.Services
{
    internal class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public async Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto)
        {
            var validator = new CreateBrandDtoValidator();
            var validatorResult = await validator.ValidateAsync(createBrandDto);

            if (validatorResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Brand Input",validatorResult);
            }

            var brand = _mapper.Map<Brand>(createBrandDto);   
            var createdEntity = await _brandRepository.CreateAsync(brand);
            var entity = _mapper.Map<BrandDto>(createdEntity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var brand =  await _brandRepository.GetByIdAsync(x=>x.Id==id);
            await _brandRepository.DeleteAsync(brand);
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brands = await _brandRepository.GetAllAsync();
            
            return _mapper.Map<List<BrandDto>>(brands);
           

        }

        public async Task<BrandDto> GetByIdAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(x=>x.Id == id);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task UpdateAsync(UpdateBrandDto updateBrandDto)
        {
            var brand = _mapper.Map<Brand>(updateBrandDto);
            await _brandRepository.UpdateAsync(brand);
        }
    }
}
