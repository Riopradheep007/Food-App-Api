using Common.Model.Authentication;
using Common.Model.Customer;
using Common.Model.Restaurent;
using DataAccess.Abstract;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Queries
{
    public  class RestaurentDataAccess: BaseDataAccess, IRestaurentDataAccess
    {
        public RestaurentDataAccess(IConfiguration configuration):base(configuration)
        {
        }

        public RestaurentInformaiton GetRestaurentInformation(int id)
        {
            string query = $@"select RestaurentName,Description,RestaurentStatus,ImagePath,PhoneNumber,Address from restaurent
                                where id = {id}";
            DbDataReader reader = GetDataReader(query, null, System.Data.CommandType.Text);
            var result = FetchResult<RestaurentInformaiton>(reader);
            return result.FirstOrDefault();

        }
        public int  AddFood(Food food)
        {
            string query = $@"insert into foods(RestaurentId,FoodName,FoodType,Price,currentlyAvailable) 
                                  select {food.Id},'{food.FoodName}','{food.FoodType}','{food.Price}','{food.CurrentlyAvailable}'
                                     where not exists(select * from foods where RestaurentId={food.Id} and foodName='{food.FoodName}');

                               select LAST_INSERT_ID() as id;";
            int foodId = Convert.ToInt32(ExecuteScalar(query, null, CommandType.Text));
            return foodId;
        }

        public void UpdateFood(UpdateFood food)
        {
            string query = $@"update foods set FoodName='{food.FoodName}',Price={food.Price},currentlyAvailable='{food.CurrentlyAvailable}'
                               where RestaurentId = {food.RestaurentId} and id = {food.Id}";
            ExecuteNonQuery(query, null, CommandType.Text);
        }

        public void UpdateRestaurentInformation(RestaurentInformation resInfo)
        {
            string query = $@"update restaurent set RestaurentName='{resInfo.RestaurentName}',description='{resInfo.Description}'
                              ,address='{resInfo.Address}',restaurentStatus = '{resInfo.RestaurentStatus}'";
            ExecuteNonQuery(query, null, CommandType.Text);
        }
        public IList<RestaurentFoods> GetAllFoods(int id)
        {
            IList<RestaurentFoods> result;
            string query = $@"select id as Id,restaurentId as RestaurentId,foodName as FoodName,
                                 foodType as FoodType,price as Price,currentlyAvailable as CurrentlyAvailable,imagePath as ImagePath
                                 from foods where restaurentId = {id}";
            DbDataReader reader = GetDataReader(query, null, System.Data.CommandType.Text);
            result = FetchResult<RestaurentFoods>(reader);
            return result;

        }
        public void deleteFood(int restaurentId, int foodId)
        {
            string query = $@"delete from foods where id = {foodId} and restaurentId = {restaurentId}";
            ExecuteNonQuery(query,null,CommandType.Text);
        }
        public void addFilePathToFood(string filePath, int foodId, int restaurentId)
        {
            string query = $@"update foods set imagePath = '{filePath}'
                                 where restaurentId = '{restaurentId}' and id = '{foodId}'";
            ExecuteNonQuery(query, null, CommandType.Text);
        }

        public IList<CustomerOrders> GetOrders(int id)
        {
            string query = $@"select id as OrderId 
                                    ,RestaurentId
                                    ,Name
                                    ,Paid
	                                ,Status
	                                ,Location
	                                ,OrderDetails
                                    ,Date
                                    from orders
                                    where restaurentId = {id} and Status <> -1";
            DbDataReader reader = GetDataReader(query, null, System.Data.CommandType.Text);
            IList<CustomerOrders> result = FetchResult<CustomerOrders>(reader);
            return result;
        }

        public void UpdateOrderStatus(OrderStatus orderStatus)
        {
            string query = $@"update orders set Status = {orderStatus.Status}
                                    where id = {orderStatus.OrderId} and restaurentId = {orderStatus.RestaurentId}";
            ExecuteNonQuery(query,null, System.Data.CommandType.Text);
                                           
        }
    }
}

