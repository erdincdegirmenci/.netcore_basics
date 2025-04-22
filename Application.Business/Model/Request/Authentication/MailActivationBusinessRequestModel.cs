using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Business.Model.Request.Authentication
{
    public class MailActivationBusinessRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string VerificationCode { get; set; }
        public DateTime? VerificationDate { get; set; }
        public DateTime? ExpireTime { get; set; }
        public string VerificationType { get; set; }
        public bool IsVerificate { get; set; }
    }
}
