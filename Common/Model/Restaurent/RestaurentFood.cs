using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.Restaurent
{
    public class Food
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public string FoodType { get; set; }
        public int Price { get; set; }
        public string CurrentlyAvailable { get; set; }
        public string? FoodImage { get; set; }
       
    }

    public class RestaurentFoods:Food
    { 
      public int RestaurentId { get; set; }
      public string ImagePath { get; set; }
    }

    public class UpdateFood
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public int Price { get; set; }
        public string CurrentlyAvailable { get; set; }
        public string? FoodImage { get; set; }
        public int RestaurentId { get; set; }
        public string ImagePath { get; set; }
        public string FoodType { get; set; }
    }

    public class RestaurentInformation
    {
        public int RestaurentId { get; set; }
        public string? RestaurentImage { get; set; }
        public string RestaurentName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string RestaurentStatus { get; set; }
       public string? ImagePath { get; set; }


    }
    public class CustomerOrders
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Paid { get; set; }
        public string OrderDetails { get; set; }
        public int RestaurentId { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
    }

    public class OrderStatus
    {
        public int OrderId { get; set; }
        public int RestaurentId { get; set; }
        public int Status { get; set; }
    }

}
