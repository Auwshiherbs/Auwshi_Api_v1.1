using AutoMapper;
using Awushi.Application.DTO.Category;
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
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepostitory _categoryRepostitory;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepostitory categoryRepostitory, IMapper mapper)
        {
            _categoryRepostitory = categoryRepostitory;
            _mapper = mapper;
        }
        public async Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            var createdEntity =  await _categoryRepostitory.CreateAsync(category);
            var entity = _mapper.Map<CategoryDto>(createdEntity);
            return entity;

        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepostitory.GetByIdAsync(x => x.Id == id);
            await  _categoryRepostitory.DeleteAsync(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var category = await _categoryRepostitory.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(category);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category =await _categoryRepostitory.GetByIdAsync(x=>x.Id == id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
             await _categoryRepostitory.UpdateAsync(category);
        }
    }
}
