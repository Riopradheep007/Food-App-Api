using Common.Model.Authentication;
using DataAccess.Abstract;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Common.Enum;
using System.Data.Common;

namespace DataAccess.Queries
{
    public  class AuthenticationDataAccess:BaseDataAccess,IAuthenticationDataAccess
    {
       
        public AuthenticationDataAccess(IConfiguration configuration) : base(configuration)
        { 
        
        }
        public bool CheckUserAlreadyExists(Login login)
        {
            string query = $@"select count(id) from {login.Role} 
                                                where emailId='{login.Email}' and password ='{login.Password}'";

            int isExists = Convert.ToInt32(ExecuteScalar(query,null,CommandType.Text));
            return isExists > 0;
        }
        public LoginResponse Login(Login login)
        {
            IList<LoginResponse> result;
            string query = $@"select Id,Name from {login.Role} where EmailId='{login.Email}' and Password='{login.Password}'";
            DbDataReader reader = GetDataReader(query, null, System.Data.CommandType.Text);
            result = FetchResult<LoginResponse>(reader);
            return result.FirstOrDefault();
        }

    }
}
