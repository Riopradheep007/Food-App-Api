using Common.Enum;
using Common.Model.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Data;

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

        [HttpGet]
        [Route("foods/{foodType}")]
        public IActionResult GetFoods([FromRoute] string foodType)
        {
            try
            {
                var customer = _customerService.Value.GetFoods(foodType);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("orders")]
        public IActionResult PlaceOrders([FromBody] List<Orders> orders)
        {
            try
            {
                _customerService.Value.PlaceOrders(orders);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
