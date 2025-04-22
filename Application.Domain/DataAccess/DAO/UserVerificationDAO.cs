using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.UserVerification;
using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Domain.DataAccess.DAO
{
    public class UserVerificationDAO : BaseDAO<IDatabaseManager>, IUserVerificationDAO
    {

        public UserVerificationDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public UserVerificationDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }
        public int CreateUserVerification(CreateUserVerificationDAOModel daoModel)
        {
            return base.InsertWithTemplate("UserVerificationDAO.CreateUserVerification", daoModel);
        }

        public GetUserVerificationDAOModel GetUserVerification(int userId)
        {
            
            GetUserVerificationDAOModel userVerification = base.SelectWithTemplate<GetUserVerificationDAOModel>("UserVerificationDAO.GetUserVerification", new { userID = userId }).FirstOrDefault();
  
            return userVerification;
        }

        public GetUserVerificationDAOModel GetUserVerificationByVerificationCode(string verificationCode)
        {
           
            GetUserVerificationDAOModel userVerification = base.SelectWithTemplate<GetUserVerificationDAOModel>("UserVerificationDAO.GetUserVerificationByVericationCode", new { VerificationCode = verificationCode }).FirstOrDefault();
           
            return userVerification;
        }

        public void UpdateUserVerification(UpdateUserVerificationDAOModel daoModel)
        {
            base.InsertWithTemplate("UserVerificationDAO.UpdateUserVerification", daoModel);
        }
    }
}
