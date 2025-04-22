using System;
using System.Collections.Generic;

namespace Application.Business.Model.Response.Authentication
{
    public class LoginBusinessResponseModel
    {
        public string Username { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
        public bool IsActive { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
