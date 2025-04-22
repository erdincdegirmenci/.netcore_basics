using Application.Domain.DataAccess.Model.Authentication;
using DefineXwork.Library.DataAccess;
using System;

namespace Application.Domain.DataAccess.DAO.Interface
{
    public interface IAuthenticationDAO : IDAO
    {
        LoginDAOModel GetLoginUser(string userName, string password);
        LoginDAOModel GetLoginUserByRefreshToken(string refreshToken, string userName);
        void RemoveRefreshToken(string userName);
        void UpdateRefreshToken(string userName, string refreshToken);
        void SetRefreshToken(string userName, string refreshToken, DateTime refreshtokencreatedate);
        LoginUserFailedDAOModel LoginUserFailed(string userName);
        void UpdateLoginFailed(UpdateLoginFailedDAOModel dAOModel);
      

    }
}
