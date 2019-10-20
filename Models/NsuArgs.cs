using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaijaStartupApp.Models
{
    public class NsuArgs
    {
        public class UserRequest
        {
            public string EmailOrUsername { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }
        public class GenericResponse
        {
            public bool IsSuccessful { get; set; }
            public string Message { get; set; }
            public List<string> Error { get; set; }
        }

        public class CreateUserRequest
        {
                public string Email { get; set; }
                public string UserName { get; set; }
                public string Password { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string Role { get; set; }
        }
    }
}
