using AutoMapper;
using Application.Business.Model.Response.Authentication;
using Application.Business.Service.Interface;
using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.Authentication;
using DefineXwork.Library.Business;
using DefineXwork.Library.Configuration;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Security;
using DefineXwork.Library.Security.Jwt;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using DefineXwork.Library.Security.Common;
using DefineXwork.Library.Logging;
using Application.Common.Extension;
using Application.Business.Model.Request.Authentication;
using Application.Domain.DataAccess.Model.UserVerification;

namespace Application.Business.Service
{
    /// <summary>
    /// Authentication Service
    /// Contains all services for authenticating users.
    /// </summary>
    /// <remarks>
    /// <para>This class can create user and company, gives access token for user to login.</para>
    /// </remarks>
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IConfigManager _configManager;
        private readonly IUserContextManager<IUserContextModel> _userContextManager;
        private readonly IAuthenticationDAO _authenticationDAO;
        private readonly IUserDAO _userDAO;
        private readonly JwtHelper _jwtHelper;


        private readonly IMapper _mapper;
        private readonly ILogManager<AuthenticationService> _logManager;

        public AuthenticationService(IUserDAO userDAO,IJwtTokenHandler jwtTokenHandler, IUserContextManager<IUserContextModel> userContextManager, IAuthenticationDAO authenticationDAO, IMapper mapper, IConfigManager configManager, ILogManager<AuthenticationService> logManager, JwtHelper jwtHelper)
        {
            _jwtTokenHandler = jwtTokenHandler;
            _userContextManager = userContextManager;
            _authenticationDAO = authenticationDAO;
            _mapper = mapper;
            _configManager = configManager;
            _logManager = logManager;
            _jwtHelper = jwtHelper;
            _userDAO = userDAO;

        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _authenticationDAO);
        }

        /// <summary>
        /// Login
        /// Gives access token to users
        /// </summary>
        /// <value>username, password</value>
        /// <returns>
        /// <para>Tokens, Username, Company Id and User Roles</para>
        /// </returns>
        public LoginBusinessResponseModel Login(string userName, string password)
        {
            LoginBusinessResponseModel result = new LoginBusinessResponseModel();
            LoginDAOModel loginUser = new LoginDAOModel();

            string hashPassword = HashingHelper.SHA256Hash(_configManager.GetConfig("AppSettings:PasswordHashKey"), password);
            loginUser = _authenticationDAO.GetLoginUser(userName, hashPassword);

            if (loginUser == null)
            {
                _logManager.LogInfo("Login Failed. User:" + userName);


                LoginUserFailedDAOModel loginFailed = _authenticationDAO.LoginUserFailed(userName);
                if (loginFailed == null)
                {
                    _logManager.LogInfo("Login Failed. User Not Found:" + userName);
                    return null;
                }
                else if (!loginFailed.IsActive)
                {
                    result.Username = userName;
                    result.IsActive = false;
                    return result;
                }
                else if ((loginFailed.LoginFailedCount + 1) >= Convert.ToInt32(_configManager.GetConfig("AppSettings:LoginFailedTryLimit")) && loginFailed.LastLoginFailedDate < DateTime.Now.AddMinutes(Convert.ToInt32(_configManager.GetConfig("AppSettings:LoginFailedTimeLimit")))) //son hatalı girişten 15 dk az olduysa ve login failed count 5 ise user passive olacak.
                {
                    _userDAO.SetUserStatus(userName, false);
                    result.Username = userName;
                    result.IsActive = false;
                    return result;
                }
                else
                {
                    if (loginFailed.LastLoginFailedDate.HasValue && loginFailed.LastLoginFailedDate.Value > DateTime.Now.AddMinutes(Convert.ToInt32(_configManager.GetConfig("AppSettings:LoginFailedTimeLimit")))) //son hatalı girişten 15 dkdan fazla olduysa count 0 a çekilir.
                    {
                        UpdateLoginFailedDAOModel updateLoginFailedDAOModel = new UpdateLoginFailedDAOModel();
                        updateLoginFailedDAOModel.LoginFailedCount = 0;
                        updateLoginFailedDAOModel.LastLoginFailedDate = null;
                        updateLoginFailedDAOModel.UserName = userName;
                        _authenticationDAO.UpdateLoginFailed(updateLoginFailedDAOModel);
                    }
                    else
                    {
                        UpdateLoginFailedDAOModel updateLoginFailedDAOModel = new UpdateLoginFailedDAOModel();
                        updateLoginFailedDAOModel.LoginFailedCount = loginFailed.LoginFailedCount+1;
                        updateLoginFailedDAOModel.LastLoginFailedDate = DateTime.Now;
                        updateLoginFailedDAOModel.UserName = userName;
                        _authenticationDAO.UpdateLoginFailed(updateLoginFailedDAOModel);
                    }
                }

                return null;

            }
            else
            {
                result = _mapper.Map<LoginDAOModel, LoginBusinessResponseModel>(loginUser);

                result.AccessToken = _jwtTokenHandler.GenerateAccessToken(result.Username, result.Roles,result.Permissions, _jwtHelper.GetJwtOptions());
                result.RefreshToken = _jwtTokenHandler.GenerateRefreshToken();

                RefreshTokenProgress(result.Username, result.RefreshToken, true);
            }

            return result;
        }

        public LoginByRefreshTokenBusinessResponseModel LoginByRefreshToken(string refreshToken, string accessToken)
        {

            LoginDAOModel loginUser = _authenticationDAO.GetLoginUserByRefreshToken(refreshToken, _jwtTokenHandler.GetUserId(accessToken));

            //user bulunamazsa veya refresh token expire olmussa
            if (loginUser == null || Convert.ToDateTime(loginUser.RefreshTokenCreateDate).AddMinutes(_jwtHelper.GetJwtOptions().RefreshTokenExpiration) < DateTime.Now)
                return null;


            var result = _mapper.Map<LoginDAOModel, LoginByRefreshTokenBusinessResponseModel>(loginUser);

            result.AccessToken = _jwtTokenHandler.GenerateAccessToken(result.Username, result.Roles,result.Permissions, _jwtHelper.GetJwtOptions());
            result.RefreshToken = refreshToken;

            return result;
        }

        /// <summary>
        /// LogOut
        /// Removes access token of user logon
        /// </summary>
        public void LogOut()
        {
            string logoutUser = _userContextManager.GetUser()?.Username;
            _authenticationDAO.RemoveRefreshToken(_userContextManager.GetUser()?.Username);
            _userContextManager.DeleteUser();
            _logManager.LogInfo("Log Out. User:" + logoutUser);
        }

        public void RefreshTokenProgress(string username, string refreshtoken, bool isInsertProgress)
        {
            if (isInsertProgress)
            {
                _authenticationDAO.SetRefreshToken(username, refreshtoken, DateTime.Now);
            }
            else
            {
                _authenticationDAO.UpdateRefreshToken(username, refreshtoken);
            }
        }

    }
}