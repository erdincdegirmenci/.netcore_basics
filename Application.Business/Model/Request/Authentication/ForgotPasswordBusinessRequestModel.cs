using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Model.Request.Authentication
{
   public class ForgotPasswordBusinessRequestModel
    {
        public string Username { get; set; }
        public string VerificationCode { get; set; }
    }
}
