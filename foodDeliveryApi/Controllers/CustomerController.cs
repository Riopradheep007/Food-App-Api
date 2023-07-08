using Common.Enum;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace foodDeliveryApi.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController: ControllerBase
    {
        private readonly Lazy<ICustomerService> _customerService;
        public CustomerController(Lazy<ICustomerService> customerService)
        {
              _customerService = customerService;
        }
/*
        [HttpGet]
        [Route("foods")]
        public IActionResult GetFoods([FromRoute] string foodType)
        {
            try
            {
                var customer = _customerService.Value.GetFoods(foodType);
            }
        }*/
    }
}
