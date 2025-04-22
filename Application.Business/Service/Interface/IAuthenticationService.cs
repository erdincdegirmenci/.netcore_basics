using Application.Business.Model.Request.Authentication;
using Application.Business.Model.Response.Authentication;
using DefineXwork.Library.Business;

namespace Application.Business.Service.Interface
{
    public interface IAuthenticationService : IService
    {
        public LoginBusinessResponseModel Login(string userName, string password);
        public LoginByRefreshTokenBusinessResponseModel LoginByRefreshToken(string refreshToken, string username);
        public void LogOut();
        public void RefreshTokenProgress(string username, string refreshtoken, bool isInsertProgress);
    }
}