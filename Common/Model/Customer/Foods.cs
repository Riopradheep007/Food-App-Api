using System;
using System.Collections.Generic;
using System.Linq;
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
}
