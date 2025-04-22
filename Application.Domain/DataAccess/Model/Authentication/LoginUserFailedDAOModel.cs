using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.DataAccess.Model.Authentication
{
    public class LoginUserFailedDAOModel
    {
        public int LoginFailedCount { get; set; }
        public DateTime? LastLoginFailedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
