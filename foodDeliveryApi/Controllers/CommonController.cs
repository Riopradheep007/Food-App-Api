using Microsoft.AspNetCore.Mvc;
using Services.Functional;
using Services.Interfaces;
using System.Reflection;

namespace foodDeliveryApi.Controllers
{
    [ApiController]
    [Route("api/Common")]
    public class CommonController: ControllerBase
    {
        private readonly Lazy<ICommonService> _commonService;
        private readonly ILogger<CommonController> _logger;
        public CommonController(Lazy<ICommonService> commonService, ILogger<CommonController> logger)
        {
            this._commonService = commonService;
            _logger = logger;
        }


        [HttpGet]
        [Route("menus/{role}")]
        public IActionResult GetMenu(string role)
        {
            try
            {
                _logger.LogInformation($"{this.GetType().Name} {MethodBase.GetCurrentMethod().Name} Function entered");
                var result = _commonService.Value.GetMenu(role);
                _logger.LogInformation($"{this.GetType().Name}  {MethodBase.GetCurrentMethod().Name} Function exited"); _logger.LogInformation($"{this.GetType().Name}  {MethodBase.GetCurrentMethod().Name} Function exited");
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }


}
