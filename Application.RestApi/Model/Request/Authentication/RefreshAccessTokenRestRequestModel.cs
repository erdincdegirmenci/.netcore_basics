namespace Application.RestApi.Model.Request.Authentication
{
    public class RefreshAccessTokenRestRequestModel
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}