using Common.Model.Authentication;
using Common.Model.Common;
using DataAccess.Abstract;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Queries
{
    public  class CommonDataAccess: BaseDataAccess,ICommonDataAccess
    {
        public CommonDataAccess(IConfiguration configuration) : base(configuration)
        {
                
        }

        public IList<SideBarMenus> GetMenu(string role)
        {
            string query = $@"select DisplayName,IconName,RouteLink from SideBarMenus where role = '{role}'";
            DbDataReader reader = GetDataReader(query, null, System.Data.CommandType.Text);
            IList<SideBarMenus>  result = FetchResult<SideBarMenus>(reader);
            return result;
        }
    }
}
