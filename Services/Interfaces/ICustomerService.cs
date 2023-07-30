using Common.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public  interface ICustomerService
    {
        public IList<Foods> GetFoods(string foodType);
        public void PlaceOrders(List<Orders> orders);
    }
}
