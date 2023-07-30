using Common.Model.Customer;
using Common.Model.Restaurent;
using foodDeliveryApi.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Functional;
using Services.Interfaces;
using System.Reflection;

namespace foodDeliveryApi.Controllers
{
    [ApiController]
    [Route("api/Restaurent")]
    public class RestaurentController : ControllerBase
    {
        private readonly Lazy<IRestaurentService> _restaurentService;
        private readonly ILogger<RestaurentController> _logger;
        private readonly Lazy<ICustomerService> _customerService;
        private readonly SignalRHub _signalRHub;
        public RestaurentController(Lazy<IRestaurentService> restaurentService,Lazy<ICustomerService> customerService,
                                       ILogger<RestaurentController> logger, SignalRHub signalRHub)
        {
            _restaurentService = restaurentService;
            _logger = logger;
            _customerService = customerService;
            _signalRHub = signalRHub;
        }
        [HttpGet]
        [Route("restaurent-information"), Authorize(Roles = "Restaurent")]
        public IActionResult GetRestaurentInformation([FromQuery] int id)
        {
            _logger.LogInformation($"{this.GetType().Name} {MethodBase.GetCurrentMethod().Name} Function entered");
            var result = _restaurentService.Value.GetRestaurentInformation(id);
            _logger.LogInformation($"{this.GetType().Name}  {MethodBase.GetCurrentMethod().Name} Function exited");
            return Ok(result);
        }

        [HttpPost]
        [Route("food"), Authorize(Roles = "Restaurent")]
        public IActionResult AddFood([FromBody] Food food)
        {
            try
            {
                _restaurentService.Value.AddFood(food);
                BroadCastFoodData();
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpPut]
        [Route("food"), Authorize(Roles = "Restaurent")]
        public IActionResult UpdateFood([FromBody] UpdateFood food)
        {
            try
            {
                _restaurentService.Value.UpdateFood(food);
                BroadCastFoodData();
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void BroadCastFoodData() {

            IList<Foods> spicesdata = _customerService.Value.GetFoods("spices");
            await this._signalRHub.BroadCastSpicesData(spicesdata);
            IList<Foods> juiceData = _customerService.Value.GetFoods("juice");
            await this._signalRHub.BroadCastJuiceData(juiceData);
            IList<Foods> iceCreamData = _customerService.Value.GetFoods("iceCream");
            await this._signalRHub.BroadCastIceCreamData(iceCreamData);
        }

        [HttpGet]
        [Route("foods"), Authorize(Roles = "Restaurent")]
        public IActionResult GetAllFoods([FromQuery] int id)
        {
            try
            {
                var result = _restaurentService.Value.GetAllFoods(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{restaurentId}/food/{foodId}"), Authorize(Roles = "Restaurent")]
        public IActionResult deleteFood([FromRoute] int restaurentId, [FromRoute] int foodId,[FromQuery] string imgPath)
        {
            try
            {
                _restaurentService.Value.DeleteFood(restaurentId,foodId,imgPath);
                BroadCastFoodData();
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("restaurent-information"), Authorize(Roles = "Restaurent")]
        public IActionResult UpdateRestaurentInformation([FromBody] RestaurentInformation restaurentInformation)
        {
            try
            {
                _restaurentService.Value.UpdateRestaurentInformation(restaurentInformation);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("orders/{id}")]
        public IActionResult GetOrders([FromRoute] int id)
        {
            try
            {
                var result = _restaurentService.Value.GetOrders(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("orders")]
        public IActionResult UpdateOrderStatus([FromBody] OrderStatus orderStatus)
        {
            try
            {
                var result = _restaurentService.Value.UpdateOrderStatus(orderStatus);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
 
}
