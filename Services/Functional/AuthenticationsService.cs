using Common.CustomException;
using Common.Enum;
using Common.Model.Authentication;
using DataAccess.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Functional
{
    public  class AuthenticationsService:IAuthenticationsService
    {
        private readonly Lazy<IAuthenticationDataAccess> _authenticationDataAccess; 
        public AuthenticationsService(Lazy<IAuthenticationDataAccess> authenticationDataAccess)
        { 
            this._authenticationDataAccess = authenticationDataAccess;
        
        }
        public LoginResponse Login(Login login)
        {
            LoginResponse result;
            bool isExists = _authenticationDataAccess.Value.CheckUserAlreadyExists(login);
            if (isExists)
            {
                result = _authenticationDataAccess.Value.Login(login);
                result.Role = login.Role;
            }
            else
            {
                throw new UserNotFoundException("user Not found");
            }

            return result;
        }
    }
}
