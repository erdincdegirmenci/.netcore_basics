using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.User;
using Application.Domain.DataAccess.Model.UserContext;
using DefineXwork.Library.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace Application.Domain.DataAccess.DAO
{
    public class UserDAO : BaseDAO<IDatabaseManager>, IUserDAO
    {
        public UserDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {
        }
        public UserDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {
        }
        public int CreateUser(CreateUserDAOModel daoModel)
        {
            return  base.InsertWithTemplate("UserDAO.CreateUser", daoModel);
        }

        public GetUserDAOModel GetUser(string userName)
        {
            
            GetUserDAOModel user = base.SelectWithTemplate<GetUserDAOModel>("UserDAO.GetUserByUsername", new { Username = userName }).FirstOrDefault();
            
            return user;
        }

        public GetUserDAOModel GetUser(int Id)
        {

            GetUserDAOModel user = base.SelectWithTemplate<GetUserDAOModel>("UserDAO.GetUserById", new { Id = Id }).FirstOrDefault();
            
            return user;
        }

        public void UpdateUserPassword(int Id, string password)
        {
            base.UpdateWithTemplate("UserDAO.UpdateUserPassword", new { Id = Id, Password = password });
        }

        public GetUserDAOModel GetActiveUser(string userName)
        {

            GetUserDAOModel user = base.SelectWithTemplate<GetUserDAOModel>("UserDAO.GetActiveUserByUsername", new { Username = userName }).FirstOrDefault();
           
            return user;
        }

        public void SetUserStatus(string userName, bool IsActive)
        {
            base.UpdateWithTemplate("UserDAO.SetUserStatus", new { Username = userName, IsActive = IsActive });
        }
    }
}