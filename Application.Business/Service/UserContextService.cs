using AutoMapper;
using Application.Business.Model.Response.UserContext;
using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.UserContext;
using DefineXwork.Library.Business;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Security.Common;
using DefineXwork.Library.Configuration;

namespace Application.Business.Service
{
    public class UserContextService : BaseService, IUserContextService
    {
        private IUserContextDAO _userContextDAO;
        private readonly IMapper _mapper;
        private readonly IConfigManager _configManager;

        // Sadece frameworkten gelen JWT authenticatin sonrası user bilgisini almak için kullanılır
        public UserContextService(IUserContextDAO userContextDAO, IMapper mapper, IConfigManager configManager)
        {
            _userContextDAO = userContextDAO;
            _mapper = mapper;
            _configManager = configManager;

        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _userContextDAO);
        }

        public UserContextModel GetUser(string userName)
        {
            UserContextUserDAOModel userDAO = _userContextDAO.GetUser(userName);
            if (userDAO == null)
                return null;
            UserContextModel user = _mapper.Map<UserContextUserDAOModel, UserContextModel>(userDAO);
            user.SetUserData<UserDetailBusinessResponseModel>(_mapper.Map<UserContextUserDAOModel, UserDetailBusinessResponseModel>(userDAO));
            return user;
        }

        public UserContextModel BasicAuthenticateUser(string username, string password)
        {
            string hashPassword = HashingHelper.SHA256Hash(_configManager.GetConfig("AppSettings:PasswordHashKey"), password);
            UserContextUserDAOModel userDAO = _userContextDAO.GetAuthenticatedUser(username, hashPassword);
            if (userDAO == null)
                return null;
            UserContextModel user = _mapper.Map<UserContextUserDAOModel, UserContextModel>(userDAO);
            user.SetUserData<UserDetailBusinessResponseModel>(_mapper.Map<UserContextUserDAOModel, UserDetailBusinessResponseModel>(userDAO));
            return user;
        }

    }
}
