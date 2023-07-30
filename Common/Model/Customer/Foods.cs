using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.Customer
{
    public  class Foods
    {
        public int FoodId { get; set; }
        public int RestaurentId { get; set; }
        public string RestaurentName { get; set; }
        public string FoodName { get; set; }
        public int Price { get; set; }
        public string CurrentlyAvailable { get; set; }
        public string FoodImagePath { get; set; } 
    }

    public class Orders
    { 
        public string Name { get; set; }
        public decimal Paid { get; set; }
        public string OrderDetails { get; set; }
        public int RestaurentId { get; set; }
        public string Location { get; set; }
    }
}
