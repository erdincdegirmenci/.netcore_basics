using System.Collections.Generic;

namespace Application.Business.Model.Response.Authentication
{
    public class RegistirationBusinessResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
    }
}
