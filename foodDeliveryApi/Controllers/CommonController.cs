using Microsoft.AspNetCore.Mvc;
using Services.Functional;
using Services.Interfaces;

namespace foodDeliveryApi.Controllers
{
    [ApiController]
    [Route("api/Common")]
    public class CommonController: ControllerBase
    {
        private readonly Lazy<ICommonService> _commonService;
        public CommonController(Lazy<ICommonService> commonService)
        {
            this._commonService = commonService;
        }


        [HttpGet]
        [Route("menus/{role}")]
        public IActionResult GetMenu(string role)
        {
            try
            {
                var result = _commonService.Value.GetMenu(role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }


}
