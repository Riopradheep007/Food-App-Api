using Common.Model.Restaurent;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface  IRestaurentService
    {
        public void AddFood(Food food);
        public void UpdateFood(UpdateFood food);
        public RestaurentInformaiton GetRestaurentInformation(int id);
        public IList<RestaurentFoods>  GetAllFoods(int id);
        public void DeleteFood(int restaurentId, int foodId, string imgPath);
        public void UpdateRestaurentInformation(RestaurentInformation restaurentInformation);

    }
}
