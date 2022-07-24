using Microsoft.AspNetCore.Mvc;
using MilitaryFinder.API.Contracts.V1;
using MilitaryFinder.API.Contracts.V1.Requests;
using MilitaryFinder.API.Contracts.V1.Responses;
using MilitaryFinder.API.Services.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace MilitaryFinder.API.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _service;

        public IdentityController(IIdentityService service)
        {
            _service = service;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            var response = await _service.RegisterAsync(request.EmailAddress, request.Password);

            if(!response.Success)
            {
                return BadRequest(new UserRegistarResponse
                {
                    Token = string.Empty,
                    ErrorMessages = response.ErrorMessages
                });
            }

            return Ok(new UserRegistarResponse
            {
                Token = response.Token,
                ErrorMessages = Enumerable.Empty<string>()
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var response = await _service.LoginAsync(request.EmailAddress, request.Password);

            if (!response.Success)
            {
                return BadRequest(new UserLoginResponse
                {
                    Token = string.Empty,
                    ErrorMessages = response.ErrorMessages
                });
            }

            return Ok(new UserLoginResponse
            {
                Token = response.Token,
                ErrorMessages = Enumerable.Empty<string>()
            }); ;
        }

    }
}
