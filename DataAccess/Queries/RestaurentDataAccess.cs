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

        public Dashboard GetDashboardData(int id)
        {

            if(CheckDashboardDateIsValid(id) == 1)
            {
                UpdateDashboardDataToZero(id);
                ClearOrdersData(id);

            }
            if (CheckRevenueDateIsValid(id) == 1)
            {
                UpdateDashboardRevenueDataToZero(id);
            }
            UpdateDashboardData(id);

            string query = $@"select  RestaurentId
                                     ,Customers
                                     ,Revenue
                                     ,PendingOrders
                                     ,DeliverdOrders
                                     ,CancelledOrders
                                     ,Monday
                                     ,Tuesday
                                     ,Wednesday
                                     ,Thursday
                                     ,Friday
                                     ,Saturday
                                     ,Sunday
                                     from dashboard
                                     where restaurentId = {id}";
            Dashboard dashboard = new Dashboard();
            List<int> revenues = new List<int>();
            DbDataReader reader = GetDataReader(query, null, System.Data.CommandType.Text);
            if (reader.Read())
            {
                dashboard.RestaurentId = Convert.ToInt32(reader["RestaurentId"]);
                dashboard.Customers = Convert.ToInt32(reader["Customers"]);
                dashboard.Revenue = Convert.ToInt32(reader["Revenue"]);
                dashboard.PendingOrders = Convert.ToInt32(reader["PendingOrders"]);
                dashboard.DeliveredOrders = Convert.ToInt32(reader["DeliverdOrders"]);
                dashboard.CancelledOrders = Convert.ToInt32(reader["CancelledOrders"]);
                revenues.Add(Convert.ToInt32(reader["Monday"]));
                revenues.Add(Convert.ToInt32(reader["Tuesday"]));
                revenues.Add(Convert.ToInt32(reader["Wednesday"]));
                revenues.Add(Convert.ToInt32(reader["Thursday"]));
                revenues.Add(Convert.ToInt32(reader["Friday"]));
                revenues.Add(Convert.ToInt32(reader["Saturday"]));
                revenues.Add(Convert.ToInt32(reader["Sunday"]));
                dashboard.Revenues = revenues;
            }

            return dashboard;

        }

        private int CheckDashboardDateIsValid(int id)
        {
            string query = $@"
                            SELECT CASE WHEN DATE(CURDATE()) > DATE(CAST(todayDate AS DATE)) THEN 1 ELSE 0 END 
                            FROM dashboard 
                            WHERE restaurentId = {id};";
            return Convert.ToInt32(ExecuteScalar(query,null,System.Data.CommandType.Text));
        }

        private void UpdateDashboardDataToZero(int id)
        {
            string query = $@"update dashboard set 
                                     customers = 0,
                                     revenue = 0,
                                     pendingOrders =  0,
                                     deliverdOrders = 0,
                                     cancelledOrders = 0,
                                     todayDate = CURDATE()
                                     where restaurentId = {id};";
            ExecuteNonQuery(query, null, System.Data.CommandType.Text);
        }

        private void ClearOrdersData(int id)
        {
            string query = $@"delete from orders where restaurentId = {id}";
            ExecuteNonQuery(query, null, System.Data.CommandType.Text);
        }
        private int CheckRevenueDateIsValid(int id)
        {
            string query = $@"SELECT CASE WHEN DATE(CURDATE()) > DATE(CAST(revenuesClearDate AS DATE)) THEN 1 ELSE 0 END 
                                FROM dashboard
                                WHERE restaurentId = {id};";
            return Convert.ToInt32(ExecuteScalar(query, null, System.Data.CommandType.Text));
        }

        private void UpdateDashboardRevenueDataToZero(int id)
        {
            string query = $@"update dashboard set 
                            Monday = 0,
                            Tuesday = 0,
                            Wednesday = 0,
                            Thursday = 0,
                            Friday = 0,
                            Saturday = 0,
                            sunday = 0,
                            revenuesClearDate = DATE_ADD(NOW(), INTERVAL 8 DAY)
                            where restaurentId = {id}";
            ExecuteNonQuery(query, null, System.Data.CommandType.Text);
        }
        private void UpdateDashboardData(int id)
        {
            string currentDay = DateTime.Now.DayOfWeek.ToString();
            string query = $@"update dashboard set 
                              customers = (select count(id)  from orders where restaurentId = {id}),
                              revenue = (select ifNull(sum(paid),0)  from orders where restaurentId = {id}),
                              pendingOrders =  (select count(id)  from orders where restaurentId = {id} and `status` = 0),
                              deliverdOrders = (select count(id)  from orders where restaurentId = {id} and `status` = 2),
                              cancelledOrders = (select count(id)  from orders where restaurentId = {id}  and `status` = -1),
                              {currentDay} = (select ifNull(sum(paid),0)  from orders where restaurentId = {id})
                              where restaurentId = {id}";

            ExecuteNonQuery(query, null, System.Data.CommandType.Text);
        }


    }
}

