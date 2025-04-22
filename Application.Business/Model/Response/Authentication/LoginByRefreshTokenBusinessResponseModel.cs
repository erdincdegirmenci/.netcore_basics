using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Model.Response.Authentication
{
   public class LoginByRefreshTokenBusinessResponseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
}
