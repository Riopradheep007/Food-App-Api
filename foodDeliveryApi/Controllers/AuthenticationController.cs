using Common.CustomException;
using Common.Model.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Interfaces;

namespace foodDeliveryApi.Controllers
{
    [ApiController]
    [Route("api/Authenticate")]
    public class AuthenticationController:ControllerBase
    {
        private readonly Lazy<IAuthenticationsService> _authenticationsService;
        public AuthenticationController(Lazy<IAuthenticationsService> authenticationsService)
        {
            _authenticationsService = authenticationsService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]Login login)
        {
            try
            {
                var result = _authenticationsService.Value.Login(login);
                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                Log.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return BadRequest("Login failed.");
        }
    }
}
