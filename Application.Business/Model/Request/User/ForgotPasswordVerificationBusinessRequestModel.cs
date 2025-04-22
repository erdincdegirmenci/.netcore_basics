using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Model.Request.User
{
    public class ForgotPasswordVerificationBusinessRequestModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
    }

}
