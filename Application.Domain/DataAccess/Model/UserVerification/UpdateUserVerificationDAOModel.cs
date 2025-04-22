using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.DataAccess.Model.UserVerification
{
    public class UpdateUserVerificationDAOModel : BaseDAOModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? VerificationDate { get; set; }
        public bool IsVerificate { get; set; }
    }
}
