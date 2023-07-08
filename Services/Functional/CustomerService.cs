using Common.Model.Customer;
using DataAccess.Interfaces;
using Microsoft.Identity.Client;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Functional
{
    public class CustomerService:ICustomerService
    {
        private readonly Lazy<ICustomerDataAccess> _customerdataAccess;
        public CustomerService(Lazy<ICustomerDataAccess> _customerdataAccess)
        {
           this._customerdataAccess = _customerdataAccess;
        }

        public IList<Foods> GetFoods(string foodType)
        { 
          var result = _customerdataAccess.Value.GetFoods(foodType);
          return result;
        }
    }
}
