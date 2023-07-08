using Common.Model.Restaurent;
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
        public RestaurentController(Lazy<IRestaurentService> restaurentService,
                                       ILogger<RestaurentController> logger)
        {
            _restaurentService = restaurentService;
            _logger = logger;
        }
        [HttpGet]
        [Route("restaurent-information")]
        public IActionResult GetRestaurentInformation([FromQuery] int id)
        {
            _logger.LogInformation($"{this.GetType().Name} {MethodBase.GetCurrentMethod().Name} Function entered");
            var result = _restaurentService.Value.GetRestaurentInformation(id);
            _logger.LogInformation($"{this.GetType().Name}  {MethodBase.GetCurrentMethod().Name} Function exited");
            return Ok(result);
        }

        [HttpPost]
        [Route("food")]
        public IActionResult AddFood([FromBody] Food food)
        {
            try
            {
                _restaurentService.Value.AddFood(food);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        [HttpPut]
        [Route("food")]
        public IActionResult UpdateFood([FromBody] UpdateFood food)
        {
            try
            {
                _restaurentService.Value.UpdateFood(food);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("foods")]
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
        [Route("{restaurentId}/food/{foodId}")]
        public IActionResult deleteFood([FromRoute] int restaurentId, [FromRoute] int foodId,[FromQuery] string imgPath)
        {
            try
            {
                _restaurentService.Value.DeleteFood(restaurentId,foodId,imgPath);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("restaurent-information")]
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
    }
 
}
