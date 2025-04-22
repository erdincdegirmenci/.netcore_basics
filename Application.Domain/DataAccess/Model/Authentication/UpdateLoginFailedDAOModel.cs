using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.DataAccess.Model.Authentication
{
    public class UpdateLoginFailedDAOModel
    {
        public string UserName { get; set; }
        public int LoginFailedCount { get; set; }
        public DateTime? LastLoginFailedDate { get; set; }

    }
}
