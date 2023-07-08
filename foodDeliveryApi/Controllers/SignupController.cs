using Common.Model.Signup;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace foodDeliveryApi.Controllers
{
    [ApiController]
    [Route("api/signup")]
    public class SignupController: ControllerBase
    {
        private readonly Lazy<ISignupService> _signupService;
        public SignupController(Lazy<ISignupService> signupService)
        {

            this._signupService = signupService;
                
        }

        [HttpPost]
        [Route("customer-signup")]
        public IActionResult CustomerSignup([FromBody] CustomerSignup customerSignup)
        {
            try
            {
                var response = _signupService.Value.CustomerSignup(customerSignup);
                return Ok(response);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        
        }

        [HttpPost]
        [Route("restaurent-signup")]
        public IActionResult RestaurentSignup([FromBody] RestaurentSignup restaurentSignup)
        {
            try
            {
                var response = _signupService.Value.RestaurentSignup(restaurentSignup);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        [Route("delivery-rider-signup")]
        public IActionResult DeliveryRiderSignup([FromBody] DeliveryRiderSignup deliveryRiderSignup)
        {
            try
            {
                var response = _signupService.Value.DeliveryRiderSignup(deliveryRiderSignup);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }





    }
}
