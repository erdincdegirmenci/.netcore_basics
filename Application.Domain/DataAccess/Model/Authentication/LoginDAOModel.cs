using System;
using System.Collections.Generic;

namespace Application.Domain.DataAccess.Model.Authentication
{
    public class LoginDAOModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenCreateDate { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
        public bool IsActive { get; set; }
    }
}
