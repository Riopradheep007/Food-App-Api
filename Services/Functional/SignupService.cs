using Common.Model.Signup;
using DataAccess.Interfaces;
using DataAccess.Queries;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Functional
{
    public class SignupService : ISignupService
    {
        private readonly Lazy<ISignupDataAccess> _signupDataAccess;
        public SignupService(Lazy<ISignupDataAccess> signupDataAccess)
        {
            this._signupDataAccess = signupDataAccess;
        }

        public string CustomerSignup(CustomerSignup customerSignup)
        {
            var result = _signupDataAccess.Value.CustomerSignup(customerSignup);
            return result;
        }

        public string RestaurentSignup(RestaurentSignup restaurentSignup)
        {
            var result = _signupDataAccess.Value.RestaurentSignup(restaurentSignup);
            return result;
        }
        public string DeliveryRiderSignup(DeliveryRiderSignup deliveryRiderSignup)
        {
            var result = _signupDataAccess.Value.DeliveryRiderSignup(deliveryRiderSignup);
            return result;
        }
    }
}
