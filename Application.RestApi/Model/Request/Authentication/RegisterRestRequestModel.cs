namespace Application.RestApi.Model.Request.Authentication
{
    public class RegisterRestRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
    }
}
