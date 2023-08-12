using Common.Enum;
using Common.Model.Customer;
using Common.Model.Restaurent;
using foodDeliveryApi.Hubs;
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
        private readonly SignalRHub _signalRHub;
        private readonly Lazy<IRestaurentService> _restaurentService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(Lazy<ICustomerService> customerService,SignalRHub signalRHub,
            Lazy<IRestaurentService> restaurentService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _signalRHub = signalRHub;
            _restaurentService = restaurentService;
            _logger = logger;
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
                SendCustomerOrdersToRestaurents(orders);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SendCustomerOrdersToRestaurents(List<Orders> orders)
        {
            foreach (var order in orders)
            {
               var result = _restaurentService.Value.GetOrders(order.RestaurentId);
               _signalRHub.SendCustomerOrders(result);
                var dashboardData = _restaurentService.Value.GetDashboardData(order.RestaurentId);
                _signalRHub.SendDashboardData(dashboardData);
            }
        }
    }
}
