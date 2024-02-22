
using Awushi.Application.DTO.Category;
using Awushi.Application.Services.Interface;
using Awushi.Domain.Contracts;
using Awushi.Domain.Models;
using Awushi.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Awushi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var categories =await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CreateCategoryDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          var entity = await _categoryService.CreateAsync(dto);
           
            return Ok(entity);
        }





        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody]UpdateCategoryDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           await _categoryService.UpdateAsync(dto);
            return NoContent();

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete]
        public async Task<ActionResult> DeleteById(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var category = await _categoryService.GetByIdAsync(id);

            if(category == null)
            {
                return BadRequest($"There is no Category on this {id}");
            }
            await _categoryService  .DeleteAsync(id); 
            return NoContent();
        }
    }
    
}
