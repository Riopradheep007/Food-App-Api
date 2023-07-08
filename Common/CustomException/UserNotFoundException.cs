using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CustomException
{
    public class UserNotFoundException:System.Exception
    {
        public UserNotFoundException(string message):base()
        {

        }
        public UserNotFoundException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
