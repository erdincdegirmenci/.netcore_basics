using Application.Domain.DataAccess.Model.UserVerification;
using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.DataAccess.DAO.Interface
{
    public interface IUserVerificationDAO : IDAO
    {
        public int CreateUserVerification(CreateUserVerificationDAOModel daoModel);
        public void UpdateUserVerification(UpdateUserVerificationDAOModel daoModel);
        public GetUserVerificationDAOModel GetUserVerification(int userId);
        public GetUserVerificationDAOModel GetUserVerificationByVerificationCode(string VerificationCode);
    }
}
