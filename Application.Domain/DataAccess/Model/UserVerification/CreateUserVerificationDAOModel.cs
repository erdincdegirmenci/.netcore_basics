using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.DataAccess.Model.UserVerification
{
    public class CreateUserVerificationDAOModel:BaseDAOModel
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string VerificationCode { get; set; }
        public DateTime? ExpireTime { get; set; }
        public string VerificationType { get; set; }
        public bool IsVerificate { get; set; }
    }
}
