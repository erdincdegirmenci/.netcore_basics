using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Model.Request.User
{
    public class CreateUserVerificationBusinessRequestModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
