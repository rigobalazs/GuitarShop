using Microsoft.AspNetCore.Mvc;
using Services.Core.Interfaces;
using Domain.View;

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestView model)
        {
            try
            {
                var response = await _authService.LoginAsync(model);
                if ( response.IsSuccess)
                {
                    return Ok(response);
                } 
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestView model)
        {
            try
            {
                var response = await _authService.Register(model);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
