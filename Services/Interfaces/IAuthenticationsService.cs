using Common.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public  interface IAuthenticationsService
    {
        public LoginResponse Login(Login login);
    }
}
