    using Awushi.Application.ApplicationConstants;
using Awushi.Application.Common;
using Awushi.Application.DTO.Brand;
using Awushi.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Awushi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        protected APIResponse _response;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
            _response = new APIResponse();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> Get()
        {
            try
            {
                var brands = await _brandService.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = brands;
            }
            catch (Exception)
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
                var brand = await _brandService.GetByIdAsync(id);
                if (brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommanMessage.RecordNotFound;
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brand;

            } catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommanMessage.SystemError);
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateBrandDto createBrandDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommanMessage.CreateOperationFailed;
                    _response.AddError(ModelState.ToString());

                }

                var brand = await _brandService.CreateAsync(createBrandDto);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommanMessage.CreateOperationSuccess;
                _response.Result = brand;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommanMessage.CreateOperationFailed;
                _response.AddError(CommanMessage.SystemError);

            }

            return _response;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateBrandDto updateBrandDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommanMessage.UpdateOperationFailed;
                    _response.AddError(ModelState.ToString());
                }
                await _brandService.UpdateAsync(updateBrandDto);
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


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        public async Task<ActionResult<APIResponse>> DeleteById(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    _response.DisplayMessage= CommanMessage.DeleteOperationFailed;
                    _response.AddError(ModelState.ToString());
                }
                var brand = await _brandService.GetByIdAsync(id);
                if (brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommanMessage.DeleteOperationFailed;
                    _response.AddError(ModelState.ToString());
                }
                await _brandService.DeleteAsync(id);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommanMessage.DeleteOperationSuccess;
            }
            catch (Exception)
            {
                _response.StatusCode= HttpStatusCode.InternalServerError;
                _response.DisplayMessage= CommanMessage.DeleteOperationFailed;
                _response.AddError(CommanMessage.SystemError);
            }
            return Ok(_response);
        }

    }
}
