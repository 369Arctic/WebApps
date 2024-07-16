using GustoGlide.Services.AuthAPI.Models.Dto;
using GustoGlide.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace GustoGlide.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _responseDto;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _responseDto = new ();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginRespoonse = await _authService.Login(model);
            if(loginRespoonse.User == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Username or password incorrect";
                return BadRequest(_responseDto);
            }
            _responseDto.Result = loginRespoonse;
            return Ok(_responseDto);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email,model.RoleName.ToUpper());
            if (!assignRoleSuccessful)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error encountered";
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}
