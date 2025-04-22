using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Model.Request.User
{
    public class RegisterUserBusinessRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
    }
}
