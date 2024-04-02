using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Awushi.Application.ApplicationConstants;
using Awushi.Application.Common;
using Awushi.Application.DTO.Product;
using Awushi.Application.InputModels;
using Awushi.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Awushi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        protected APIResponse _response;
        //private readonly IWebHostEnvironment _hostEnvironment;
        //private readonly IConfiguration _configuration;
        //private readonly IAmazonS3 _s3Client;
        public ProductController(IProductService productService)
        {
            _productService = productService;
            _response = new APIResponse();
            //this._hostEnvironment = hostEnvironment;
            //_configuration = configuration;
            //_s3Client = s3Client;
        }

        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> Get()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = products;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommanMessage.SystemError);
            }

            return _response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<APIResponse>> GetByPagination(PaginationInputModel paginationInputModel)
        {
            try
            {
                var products = await _productService.GetPagination(paginationInputModel);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = products;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommanMessage.SystemError);
            }

            return _response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("Filter")]
        public async Task<ActionResult<APIResponse>> GetFilter(int? categoryId, int? brandId, double? price)
        {
            try
            {
                var products = await _productService.GetAllByFilterAsync(categoryId, brandId, price);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = products;
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
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.AddError(CommanMessage.RecordNotFound);
                }
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = product;
            }
            catch (Exception)
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
        public async Task<ActionResult<APIResponse>> Create([FromForm] CreateProductDto createProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommanMessage.CreateOperationFailed;
                    _response.AddError(ModelState.ToString());
                    return BadRequest(_response);
                }

                // Save image to S3 bucket
                //createProductDto.ImageName = await SaveImage(createProductDto.ImageFile);

                var product = await _productService.CreateAsync(createProductDto);
                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommanMessage.CreateOperationSuccess;
                _response.Result = product;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommanMessage.CreateOperationSuccess;
                _response.AddError(CommanMessage.SystemError);

            }
            return _response;

        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Update([FromBody]UpdateProductDto updateProductDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessage = CommanMessage.UpdateOperationFailed;
                    _response.AddError(ModelState.ToString());
                }
                await _productService.UpdateAsync(updateProductDto);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommanMessage.UpdateOperationSuccess;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessage = CommanMessage.CreateOperationFailed;
                _response.AddError(CommanMessage.SystemError);
            }
            return Ok(_response);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessage = CommanMessage.DeleteOperationFailed;
                    _response.AddError(ModelState.ToString());
                }
                await _productService.DeleteAsync(id);
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
            return _response;
        }

        //[NonAction]
        //public async Task<string> SaveImage(IFormFile imagefile)
        //{
        //    try
        //    {
        //        if (imagefile==null || imagefile.Length<=0)
        //        {
        //            throw new ArgumentException("invalid image file");
        //        }

        //        //var AwsAccessKey = _configuration["AwsConfiguration:AwsAccessKey"];
        //        //var AwsSecretKey = _configuration["AwsConfiguration:AwsSecretKey"];
        //        //var AwsBucketName = _configuration["AwsConfiguration:AwsBucketName"];
        //        //var Region = _configuration["AwsConfiguration:Region"];

        //        // Initialize AmazonS3Client with credentials and region
        //        var s3Client = new AmazonS3Client(AwsAccessKey, AwsSecretKey, RegionEndpoint.GetBySystemName(Region));
        //        var key = Guid.NewGuid().ToString();

        //        using (var stream = imagefile.OpenReadStream())
        //        {
        //            var putRequest = new PutObjectRequest
        //            {
        //                BucketName = AwsBucketName,
        //                Key = key,
        //                InputStream = stream,
        //                ContentType = imagefile.ContentType
        //            };
        //            await _s3Client.PutObjectAsync(putRequest);
        //        }
        //        return key;
        //    }
        //    catch(Exception ) 
        //    {
        //        throw;
        //    }
        //}
    }
}
