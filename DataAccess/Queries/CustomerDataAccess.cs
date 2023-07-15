using Common.Model.Customer;
using Common.Model.Restaurent;
using DataAccess.Abstract;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Queries
{
    public class CustomerDataAccess : BaseDataAccess,ICustomerDataAccess
    {
        public CustomerDataAccess(IConfiguration configuration) : base(configuration)
        {
                
        }

        public IList<Foods> GetFoods(string foodType)
        {
            string query = $@" select f.id as FoodId,
                                       res.restaurentName as RestaurentName 
                                      ,f.RestaurentId as RestaurentId
                                      ,f.FoodName as FoodName
                                      ,f.Price as Price
                                      ,f.CurrentlyAvailable as CurrentlyAvailable
                                      ,f.imagePath as FoodImagePath 
                                         from foods f
		                                 inner join  restaurent res 
                                            on f.restaurentId = res.id
		                                 where  f.foodType = '{foodType}'";
            DbDataReader reader = GetDataReader(query, null, System.Data.CommandType.Text);
            var result = FetchResult<Foods>(reader);
            return result;
        }


    }
}
