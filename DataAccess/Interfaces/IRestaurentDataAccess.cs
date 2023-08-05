using Common.Model.Authentication;
using Common.Model.Customer;
using Common.Model.Restaurent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public  interface IRestaurentDataAccess
    {
        public int  AddFood(Food food);
        public void UpdateFood(UpdateFood food);
        public RestaurentInformaiton GetRestaurentInformation(int id);
        public IList<RestaurentFoods> GetAllFoods(int id);
        public void addFilePathToFood(string filePath,int foodId,int restaurentId);
        public void deleteFood(int restaurentId, int foodId);
        public void UpdateRestaurentInformation(RestaurentInformation restaurentInformation);
        public IList<CustomerOrders> GetOrders(int id);
        public void  UpdateOrderStatus(OrderStatus orderStatus);
        public Dashboard GetDashboardData(int id);
    }
}
