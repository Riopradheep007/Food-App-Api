using Common.Model.Signup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public  interface ISignupService
    {

        public string  CustomerSignup(CustomerSignup customerSignup);

        public string RestaurentSignup(RestaurentSignup restaurentSignup);
        public string DeliveryRiderSignup(DeliveryRiderSignup restaurentSignup);
    }
}
