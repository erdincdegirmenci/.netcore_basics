using System.Collections.Generic;

namespace Application.RestApi.Model.Response
{
    public class LoginRestResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
}
