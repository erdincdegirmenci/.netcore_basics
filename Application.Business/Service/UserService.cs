using AutoMapper;
using Application.Business.Model.Request.User;
using Application.Business.Model.Response.User;
using Application.Business.Service.Interface;
using Application.Common.Const;
using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.User;
using DefineXwork.Library.Business;
using DefineXwork.Library.Configuration;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.Security;
using DefineXwork.Library.Security.Common;
using System;
using Application.Common.Helper;
using Application.Domain.DataAccess.Model.UserVerification;
using Application.Business.Common.Enum;
using Application.Common.Models;
using DefineXwork.Library.DataAccess.Manager;

namespace Application.Business.Service
{
    public class UserService : BaseService, IUserService
    {
        private readonly IConfigManager _configManager;
        private readonly IMapper _mapper;
        private readonly IUserDAO _userDAO;
        private readonly IUserVerificationDAO _userVerificationDAO;
        private readonly IUserContextManager<IUserContextModel> _userContextManager;
        private readonly EmailHelper _emailHelper;
        public UserService(IUserDAO userDAO, IMapper mapper, IUserContextManager<IUserContextModel> userContextManager, IConfigManager configManager, IUserVerificationDAO userVerificationDAO, EmailHelper emailHelper)
        {
            _userContextManager = userContextManager;
            _configManager = configManager;
            _mapper = mapper;
            _userDAO = userDAO;
            _userVerificationDAO = userVerificationDAO;
            _emailHelper = emailHelper;
        }
        public void AddToExternalTransaction(IDatabaseManager databaseManager)
        {
            base.AddToTransaction(databaseManager, _userDAO);
        }

        #region Execute Methods
        public int CreateUser(CreateUserBusinessRequestModel createUserBusinessServiceRequestModel)
        {
            var userDAOModel = _mapper.Map<CreateUserBusinessRequestModel, CreateUserDAOModel>(createUserBusinessServiceRequestModel);

            var isUserExists = CheckIsUserExist(userDAOModel.Username);
            if (isUserExists) return 0;


            string hashPassword = HashingHelper.SHA256Hash(_configManager.GetConfig("AppSettings:PasswordHashKey"), createUserBusinessServiceRequestModel.Password);
            userDAOModel.Password = hashPassword;
            userDAOModel.UserType = createUserBusinessServiceRequestModel.UserType;
            userDAOModel.IsActive = false;//Registeration mailinden sonra isactive true yapılacak.

            userDAOModel.IsDeleted = false;
            userDAOModel.CreateDate = DateTime.Now;
            userDAOModel.CreateUser = _userContextManager.GetUser()?.Username;


            int userId = _userDAO.CreateUser(userDAOModel);


            return userId;
        }
        public GetActiveUserInfoBusinessResponseModel GetActiveUser(string username)
        {
            var userInfoInDb = _userDAO.GetActiveUser(username);
            var userDAOModel = _mapper.Map<GetUserDAOModel, GetActiveUserInfoBusinessResponseModel>(userInfoInDb);
            return userDAOModel;
        }
        public GetActiveUserInfoBusinessResponseModel GetUser(int Id)
        {
            var userInfoInDb = _userDAO.GetUser(Id);
            var userDAOModel = _mapper.Map<GetUserDAOModel, GetActiveUserInfoBusinessResponseModel>(userInfoInDb);
            return userDAOModel;
        }
        public void UpdateUserPassword(int userId, string password)
        {
            string hashPassword = HashingHelper.SHA256Hash(_configManager.GetConfig("AppSettings:PasswordHashKey"), password);
            _userDAO.UpdateUserPassword(userId, hashPassword);
        }

        public void SetUserStatus(string userName, bool IsActive)
        {
            _userDAO.SetUserStatus(userName, IsActive);
        }


        #endregion

        #region Verification
        public GetUserVerificationBusinessResponseModel GetUserVerificationByUserId(GetUserVerificationBusinessRequestModel request)
        {
            GetUserVerificationDAOModel userVerification = _userVerificationDAO.GetUserVerification(request.UserId);

            if (userVerification == null)
                return null;

            var result = _mapper.Map<GetUserVerificationDAOModel, GetUserVerificationBusinessResponseModel>(userVerification);
            return result;
        }

        public int CreateUserVerification(CreateUserVerificationBusinessRequestModel request)
        {
            string hashVerificationCode = HashingHelper.Base64StringEncode(Guid.NewGuid().ToString());

            var userVerificationDAOModel = _mapper.Map<CreateUserVerificationBusinessRequestModel, CreateUserVerificationDAOModel>(request);

            userVerificationDAOModel.VerificationCode = hashVerificationCode;
            userVerificationDAOModel.ExpireTime = DateTime.Now.AddDays(int.Parse(_configManager.GetConfig("AppSettings:MailExpireDay")));
            userVerificationDAOModel.CreateDate = DateTime.Now;
            userVerificationDAOModel.CreateUser = _userContextManager.GetUser()?.Username;
            userVerificationDAOModel.IsVerificate = false;
            userVerificationDAOModel.VerificationType = ((int)VerificationType.Register).ToString();

            int createdUserVerificationId = _userVerificationDAO.CreateUserVerification(userVerificationDAOModel);

            if (createdUserVerificationId > 0)
            {
                var message = new EmailMessage(
                    new string[] { request.Username },
                    "Test Verification Mail",
                    string.Format("<a href='{0}/mailactivation?Mode=register&VerificationCode={1}'>Şifre Aktivasyon için Tıklayınız</a>", _configManager.GetConfig("AppSettings:FrontEndUrl"), hashVerificationCode));
                _emailHelper.SendEmail(message);
            }


            return createdUserVerificationId;
        }

        public int ForgotPasswordVerification(ForgotPasswordVerificationBusinessRequestModel request)
        {
            string hashVerificationCode = HashingHelper.Base64StringEncode(Guid.NewGuid().ToString());

            var userVerificationDAOModel = _mapper.Map<ForgotPasswordVerificationBusinessRequestModel, CreateUserVerificationDAOModel>(request);

            userVerificationDAOModel.VerificationCode = hashVerificationCode;
            userVerificationDAOModel.ExpireTime = DateTime.Now.AddDays(int.Parse(_configManager.GetConfig("AppSettings:MailExpireDay")));
            userVerificationDAOModel.CreateDate = DateTime.Now;
            userVerificationDAOModel.CreateUser = _userContextManager.GetUser()?.Username;
            userVerificationDAOModel.IsVerificate = false;
            userVerificationDAOModel.VerificationType = ((int)VerificationType.ForgotPassword).ToString();

            int createdUserVerificationId = _userVerificationDAO.CreateUserVerification(userVerificationDAOModel);

            if (createdUserVerificationId > 0)
            {
                var message = new EmailMessage(
                     new string[] { request.Username },
                     "Test Forgot Password Mail",
                     string.Format("<a href='{0}/mailactivation?Mode=forgot&VerificationCode={1}'>Şifre Sıfırlama için Tıklayınız</a>", _configManager.GetConfig("AppSettings:FrontEndUrl"), hashVerificationCode)
                     );
                _emailHelper.SendEmail(message);
            }


            return createdUserVerificationId;
        }

        public void UpdateUserVerification(UpdateUserVerificationBusinessRequestModel request)
        {


            var userVerificationDAOModel = _mapper.Map<UpdateUserVerificationBusinessRequestModel, UpdateUserVerificationDAOModel>(request);
            userVerificationDAOModel.LastUpdateDate = DateTime.Now;
            userVerificationDAOModel.LastUpdateUser = _userContextManager.GetUser()?.Username;
            userVerificationDAOModel.VerificationDate = DateTime.Now;

            _userVerificationDAO.UpdateUserVerification(userVerificationDAOModel);
        }

        public GetUserVerificationBusinessResponseModel GetUserVerificationByVerificationCode(string verificationCode)
        {
            GetUserVerificationDAOModel userVerification = _userVerificationDAO.GetUserVerificationByVerificationCode(verificationCode);


            var result = _mapper.Map<GetUserVerificationDAOModel, GetUserVerificationBusinessResponseModel>(userVerification);
            return result;
        }

        public int RegisterUser(RegisterUserBusinessRequestModel request)
        {

            using (IDatabaseManager databaseManager = new PostgreSqlDatabaseManager("DbConnection", _configManager))
            {
                int createUserServiceResult = 0;
                try
                {
                    databaseManager.AddToTransaction(_userDAO, _userVerificationDAO);

                    databaseManager.BeginTransaction();

                    #region Create User
                    var createUserServiceRequestModel = _mapper.Map<RegisterUserBusinessRequestModel, CreateUserBusinessRequestModel>(request);
                    createUserServiceResult = CreateUser(createUserServiceRequestModel);
                    #endregion
                    if (createUserServiceResult == 0)
                    {
                        return 0;
                    }

                    #region UserVerification
                    CreateUserVerificationBusinessRequestModel createUserVerificationBusinessRequestModel = new CreateUserVerificationBusinessRequestModel();
                    createUserVerificationBusinessRequestModel.UserId = createUserServiceResult;
                    createUserVerificationBusinessRequestModel.Username = createUserServiceRequestModel.Username;

                    CreateUserVerification(createUserVerificationBusinessRequestModel);

                    #endregion

                    databaseManager.CommitTransaction();
                }
                catch (Exception ex)
                {
                    databaseManager.RollbackTransaction();

                    throw;
                }


                return createUserServiceResult;

            }
        }

        public void ActivateUserByMailVerifcation(int verificatonId, int userId, string username)
        {

            using (IDatabaseManager databaseManager = new PostgreSqlDatabaseManager("DbConnection", _configManager))
            {
                //TODO:Requestte alınan mode ne ise yarıyor
                try
                {
                    databaseManager.AddToTransaction(_userDAO, _userVerificationDAO);

                    databaseManager.BeginTransaction();

                    SetUserStatus(username, true);

                    UpdateUserVerificationBusinessRequestModel updateUserVerificationBusinessRequestModel = new UpdateUserVerificationBusinessRequestModel();
                    updateUserVerificationBusinessRequestModel.Id = verificatonId;
                    updateUserVerificationBusinessRequestModel.UserId = userId;
                    updateUserVerificationBusinessRequestModel.IsVerificate = true;
                    UpdateUserVerification(updateUserVerificationBusinessRequestModel);


                    databaseManager.CommitTransaction();
                }
                catch (Exception ex)
                {
                    databaseManager.RollbackTransaction();

                    throw;
                }
            }
        }

        public void ChangeUserPasswordByMailVerification(int verificatonId, int userId, string password)
        {

            using (IDatabaseManager databaseManager = new PostgreSqlDatabaseManager("DbConnection", _configManager))
            {
                //TODO:Requestte alınan mode ne ise yarıyor
                try
                {
                    databaseManager.AddToTransaction(_userDAO, _userVerificationDAO);

                    databaseManager.BeginTransaction();

                    UpdateUserVerificationBusinessRequestModel updateUserVerificationBusinessRequestModel = new UpdateUserVerificationBusinessRequestModel();
                    updateUserVerificationBusinessRequestModel.Id = verificatonId;
                    updateUserVerificationBusinessRequestModel.UserId = userId;
                    updateUserVerificationBusinessRequestModel.IsVerificate = true;
                    UpdateUserVerification(updateUserVerificationBusinessRequestModel);


                    #region User Password Change
                    UpdateUserPassword(userId, password);
                    #endregion

                    databaseManager.CommitTransaction();
                }
                catch (Exception ex)
                {
                    databaseManager.RollbackTransaction();

                    throw;
                }

            }
        }
        #endregion

        #region Private Methods

        private bool CheckIsUserExist(string username)
        {
            var userInDb = _userDAO.GetUser(username);
            return userInDb != null;
        }
        #endregion
    }
}
