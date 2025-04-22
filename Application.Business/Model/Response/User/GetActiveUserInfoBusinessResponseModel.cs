using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Model.Response.User
{
   public class GetActiveUserInfoBusinessResponseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
    }
}
