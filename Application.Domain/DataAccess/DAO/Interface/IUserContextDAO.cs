using Application.Domain.DataAccess.Model.UserContext;
using DefineXwork.Library.DataAccess;

namespace Application.Domain.DataAccess.DAO.Interface
{
    public interface IUserContextDAO : IDAO
    {
        public UserContextUserDAOModel GetUser(string userName);
        UserContextUserDAOModel GetAuthenticatedUser(string username, string password);
    }
}
