using Common.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public  interface IAuthenticationDataAccess
    {
        public bool CheckUserAlreadyExists(Login login);
        public LoginResponse Login(Login login);
    }
}
