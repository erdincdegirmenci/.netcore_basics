using System.Collections.Generic;

namespace Application.Business.Model.Response.UserContext
{
    public class UserDetailBusinessResponseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
}
