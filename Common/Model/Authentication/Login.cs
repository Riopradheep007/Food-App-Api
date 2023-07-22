using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model.Authentication
{
    public  class Login
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class LoginResponse
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string? Role { get; set; }
        public string? token { get; set; }
    }
}
