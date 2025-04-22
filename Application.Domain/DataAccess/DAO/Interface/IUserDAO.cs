using Application.Domain.DataAccess.Model.User;
using DefineXwork.Library.DataAccess;

namespace Application.Domain.DataAccess.DAO.Interface
{
    public interface IUserDAO : IDAO
    {
        int CreateUser(CreateUserDAOModel daoModel);
        GetUserDAOModel GetUser(string userName);
        GetUserDAOModel GetUser(int Id);
        GetUserDAOModel GetActiveUser(string userName);
        void UpdateUserPassword(int userId, string password);
        void SetUserStatus(string userName, bool IsActive);


    }
}
