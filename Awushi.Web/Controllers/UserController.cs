using Awushi.Application.ApplicationConstants;
using Awushi.Application.Common;
using Awushi.Application.InputModels;
using Awushi.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Awushi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly APIResponse _response;

        public UserController(IAuthService authService)
        {
            _authService = authService;
            _response = new APIResponse();
        }


        [HttpPost]

        public async Task<ActionResult<APIResponse>> Register(Register register)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddError(CommanMessage.RegistrationFailed);
                    return _response;
                }
                var user = await _authService.Register(register);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.DisplayMessage = CommanMessage.RegistrationSuccess;
                _response.Result = user;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommanMessage.SystemError);
            }

            return Ok(_response);
        }
    }
}
