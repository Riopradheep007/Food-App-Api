using Common.CustomException;
using Common.Model.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;

namespace foodDeliveryApi.Controllers
{
    [ApiController]
    [Route("api/Authenticate")]
    public class AuthenticationController:ControllerBase
    {
        private readonly Lazy<IAuthenticationsService> _authenticationsService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController>  _logger;
        public AuthenticationController(IConfiguration configuration, 
              Lazy<IAuthenticationsService> authenticationsService,
              ILogger<AuthenticationController> logger)
        {
            _configuration = configuration;
            _authenticationsService = authenticationsService;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]Login login)
        {
            try
            {
                _logger.LogInformation($"{this.GetType().Name} {MethodBase.GetCurrentMethod().Name} Function entered");
                var result = _authenticationsService.Value.Login(login);
                result.token = CreateToken(login);
                _logger.LogInformation($"{this.GetType().Name}  {MethodBase.GetCurrentMethod().Name} Function exited");
                return Ok(result);
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError(ex.Message);
;            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest("Login failed.");
        }

        private string CreateToken(Login user)
        {
            _logger.LogInformation($"{this.GetType().Name} {MethodBase.GetCurrentMethod().Name} Function entered");
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation($"{this.GetType().Name}  {MethodBase.GetCurrentMethod().Name} Function exited");
            return jwt;
        }
    }
}
