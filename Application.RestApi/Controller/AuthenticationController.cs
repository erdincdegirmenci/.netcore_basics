using AutoMapper;
using Application.Business.Model.Request.Authentication;
using Application.Business.Model.Response;
using Application.Business.Model.Response.Authentication;
using Application.Business.Service.Interface;
using Application.Common.Const;
using Application.RestApi.Model.Request.Authentication;
using Application.RestApi.Model.Response;
using DefineXwork.Library.Logging;
using DefineXwork.Library.Security.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DefineXwork.Library.DataAccess;
using DefineXwork.Library.DataAccess.Manager;
using Application.Business.Model.Request.User;
using System;
using Application.Business.Model.Response.User;
using DefineXwork.Library.Configuration;

namespace Application.RestApi.Controller
{
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly ILogManager<AuthenticationController> _logManager;
        private readonly IUserContextService _userContextService;

        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IConfigManager _configManager;
        private readonly IMapper _mapper;

        public AuthenticationController(ILogManager<AuthenticationController> logManager, IUserContextService userContextService, IAuthenticationService authenticationService, IMapper mapper, IUserService userService, IConfigManager configManager)
        {
            _logManager = logManager;
            _authenticationService = authenticationService;
            _userContextService = userContextService;
            _userService = userService;
            _configManager = configManager;
            _mapper = mapper;
        }

        ///////////////////////////////////////Authorize etiketi kullanımı ////////////////////

        /// [Authorize] --> Sadece kullanıcının login olmasını kontrol eder
        /// [Authorize(Roles="Admin")] --> Kullanıcının Admin rolünü kontrol eder.
        /// [Authorize(Roles="_List")] --> Kullanıcının List yetkisini kontrol eder. Yetkiye göre kontrol yapılacaksa yetki key inin önüne "_" konur. Bu şekilde aynı yerden rol ve yetki kontrolü yapılabilir.
        //////////////////////////////////

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRestRequestModel loginRestRequest)
        {
            RestResponseContainer<LoginRestResponseModel> response = new RestResponseContainer<LoginRestResponseModel>();

            LoginBusinessResponseModel loginUser = _authenticationService.Login(loginRestRequest.Username, loginRestRequest.Password);

            if (loginUser == null)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.LOGIN_FAILED;
            }
            else if (!loginUser.IsActive)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.USER_PASSIVE;
            }
            else
            {
                response.IsSucceed = true;
                response.Response = _mapper.Map<LoginBusinessResponseModel, LoginRestResponseModel>(loginUser);
            }

            return Ok(response);

        }

        /// <summary>
        /// **** Dummy data dönüyor şu an
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public IActionResult Register([FromBody] RegisterRestRequestModel request)
        {

            RestResponseContainer<EmptyRestResponseModel> response = new RestResponseContainer<EmptyRestResponseModel>();

            var registerUserServiceRequestModel = _mapper.Map<RegisterRestRequestModel, RegisterUserBusinessRequestModel>(request);
            var registerUserServiceResult = _userService.RegisterUser(registerUserServiceRequestModel);

            if (registerUserServiceResult == 0)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.USER_ALREADY_EXIST;

                return Ok(response);
            }

            response.IsSucceed = true;

            return Ok(response);
        }
      
        [HttpPost]
        [Route("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            RestResponseContainer<EmptyRestResponseModel> response = new RestResponseContainer<EmptyRestResponseModel>();
            _authenticationService.LogOut();
            response.IsSucceed = true;
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RefreshAccessToken")]
        public IActionResult RefreshAccessToken([FromBody] RefreshAccessTokenRestRequestModel request)
        {
            RestResponseContainer<LoginRestResponseModel> response = new RestResponseContainer<LoginRestResponseModel>();

            var loginUser = _authenticationService.LoginByRefreshToken(request.RefreshToken, request.AccessToken);

            if (loginUser == null)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.LOGIN_FAILED;

                return Ok(response);
            }

            response.IsSucceed = true;
            response.Response = _mapper.Map<LoginByRefreshTokenBusinessResponseModel, LoginRestResponseModel>(loginUser);

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordRestRequestModel request)
        {
            RestResponseContainer<EmptyRestResponseModel> response = new RestResponseContainer<EmptyRestResponseModel>();

            GetActiveUserInfoBusinessResponseModel activeUser = _userService.GetActiveUser(request.Username);
            if (activeUser == null)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.USER_NO_DATA_FOUND;
                return Ok(response);
            }

            #region UserVerification
            ForgotPasswordVerificationBusinessRequestModel forgotPasswordVerificationBusinessRequestModel = new ForgotPasswordVerificationBusinessRequestModel();
            forgotPasswordVerificationBusinessRequestModel.UserId = activeUser.Id;
            forgotPasswordVerificationBusinessRequestModel.Username = activeUser.Username;
            _userService.ForgotPasswordVerification(forgotPasswordVerificationBusinessRequestModel);
            #endregion
            response.IsSucceed = true;

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("MailActivation")]
        public IActionResult MailActivation([FromBody] MailActivationRestRequestModel request)
        {

            RestResponseContainer<EmptyRestResponseModel> response = new RestResponseContainer<EmptyRestResponseModel>();




            #region Control Verification Code
            var userVerificationResult = _userService.GetUserVerificationByVerificationCode(request.VerificationCode);
            #endregion
            if (userVerificationResult == null || userVerificationResult.Id == 0)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.USER_NO_DATA_FOUND;
                return Ok(response);
            }
            //TODO:Buradaki eski mantıgı düzelttim. Efe beraber bakalım
            else if ((userVerificationResult.IsVerificate && !string.IsNullOrEmpty(userVerificationResult.VerificationDate.ToString())) || userVerificationResult.ExpireTime < DateTime.Now)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.MAIL_LINK_USED_OR_EXPIRE;
                return Ok(response);
            }


            #region Update User And Verification status
            GetActiveUserInfoBusinessResponseModel activationUser = _userService.GetUser(userVerificationResult.UserId);
            if (activationUser == null)
            {
                response.IsSucceed = false;
                response.ErrorCode = MessageCodes.USER_NO_DATA_FOUND;
                return Ok(response);
            }
            else
            {
                _userService.ActivateUserByMailVerifcation(userVerificationResult.Id, userVerificationResult.UserId, activationUser.Username);
            }


            response.IsSucceed = true;


            #endregion


            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordRestRequestModel request)
        {

            RestResponseContainer<EmptyRestResponseModel> response = new RestResponseContainer<EmptyRestResponseModel>();


                    #region Control Verification Code
                    var userVerificationResult = _userService.GetUserVerificationByVerificationCode(request.VerificationCode);
                    #endregion
                    if (userVerificationResult == null || userVerificationResult.Id == 0)
                    {
                        response.IsSucceed = false;
                        response.ErrorCode = MessageCodes.USER_NO_DATA_FOUND;
                        return Ok(response);
                    }
                    else if ((userVerificationResult.IsVerificate && !string.IsNullOrEmpty(userVerificationResult.VerificationDate.ToString())) || userVerificationResult.ExpireTime < DateTime.Now)
                    {
                        response.IsSucceed = false;
                        response.ErrorCode = MessageCodes.MAIL_LINK_USED_OR_EXPIRE;
                        return Ok(response);
                    }
                    #region Update User And Verification status
                    GetActiveUserInfoBusinessResponseModel activationUser = _userService.GetUser(userVerificationResult.UserId);
                    if (activationUser == null)
                    {
                        response.IsSucceed = false;
                        response.ErrorCode = MessageCodes.USER_NO_DATA_FOUND;
                        return Ok(response);
                    }
                    else
                    {
                        _userService.ChangeUserPasswordByMailVerification(userVerificationResult.Id, userVerificationResult.UserId, request.Password);
                    }
                    #endregion

                 
                    response.IsSucceed = true;
            

            return Ok(response);
        }

        //Frontend uygulamasına ilk giriste logine düsmesi için kontrol edilir.
        [HttpPost]
        [Route("IsLogin")]
        [Authorize]
        public IActionResult IsLogin()
        {
            RestResponseContainer<EmptyRestResponseModel> response = new RestResponseContainer<EmptyRestResponseModel>();
            response.IsSucceed = true;
            return Ok(response);
        }
    }
}