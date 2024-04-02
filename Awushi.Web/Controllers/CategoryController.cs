
using Awushi.Application.ApplicationConstants;
using Awushi.Application.Common;
using Awushi.Application.DTO.Category;
using Awushi.Application.Services.Interface;
using Awushi.Domain.Contracts;
using Awushi.Domain.Models;
using Awushi.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Awushi.Web.Controllers
{
 //   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        protected APIResponse _response;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> Get()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = categories;
            }
            catch(Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommanMessage.SystemError);
            }

            return _response;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult<APIResponse>> GetById(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null)
                {

                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommanMessage.RecordNotFound;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = category;
               
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommanMessage.SystemError);
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody]CreateCategoryDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommanMessage.CreateOperationFailed; 
                    _response.AddError(ModelState.ToString());
                }
                var entity = await _categoryService.CreateAsync(dto);
                _response.StatusCode=HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommanMessage.CreateOperationSuccess;
                _response.Result = entity;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage= CommanMessage.CreateOperationFailed;
                _response.AddError(CommanMessage.SystemError);

            }
           
            return _response ;
        }






        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Update([FromBody]UpdateCategoryDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommanMessage.UpdateOperationFailed;
                    _response.AddError(ModelState.ToString());
                }

                await _categoryService.UpdateAsync(dto);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommanMessage.UpdateOperationSuccess;
                
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommanMessage.UpdateOperationFailed;
                _response.AddError(CommanMessage.SystemError);
            }
            return Ok(_response);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<ActionResult<APIResponse>> DeleteById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommanMessage.DeleteOperationFailed;
                    _response.AddError(ModelState.ToString());
                }
                var category = await _categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommanMessage.DeleteOperationFailed;
                    _response.AddError(ModelState.ToString());
                }
                await _categoryService.DeleteAsync(id);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommanMessage.DeleteOperationSuccess;
            } 
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommanMessage.DeleteOperationFailed;
                _response.AddError(CommanMessage.SystemError);  
            }
            return Ok(_response);
        }
    }
    
}
