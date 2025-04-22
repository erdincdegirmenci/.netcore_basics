using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.Authentication;
using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Application.Domain.DataAccess.DAO
{
    public class AuthenticationDAO : BaseDAO<IDatabaseManager>, IAuthenticationDAO
    {
        public AuthenticationDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public AuthenticationDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }

        public LoginDAOModel GetLoginUser(string username, string password)
        {
            LoginDAOModel user = base.SelectWithTemplate<LoginDAOModel>("AuthenticationDAO.GetLoginUser", new { Username = username, Password = password }).FirstOrDefault();
            if (user == null) return null;
            List<LoginRoleDAOModel> userRoles = base.SelectWithTemplate<LoginRoleDAOModel>("AuthenticationDAO.GetloginUserRoles", new { UserId = user.Id }).ToList();

            if (userRoles.Count > 0)
                user.Roles = userRoles.Select(x => x.Role).ToList();

           
            List<LoginPermissionDAOModel> userPermissions = base.SelectWithTemplate<LoginPermissionDAOModel>("AuthenticationDAO.GetloginUserPermissions", new { UserId = user.Id }).ToList();
            List<LoginRolePermissionDAOModel> rolePermissions = base.SelectWithTemplate<LoginRolePermissionDAOModel>("AuthenticationDAO.GetloginUserRolePermissions", new { UserId = user.Id }).ToList();

            user.Permissions = new List<string>();

            if (rolePermissions.Count > 0)
            {
                user.Permissions.AddRange(rolePermissions.Select(x => x.Permission).ToList());
            }

            //Rolden gelen yetkilere ek olarak kullanıcı üzerinde verilen yetkiler eklenir.
            //IsDeny=true olanlar ise yetkilerden çıkarılır

            if (userPermissions.Where(x => !x.IsDeny).ToList().Count > 0)
                user.Permissions.AddRange(userPermissions.Where(x => !x.IsDeny).Select(x => x.Permission).ToList());


            foreach (var denyPermission in userPermissions.Where(x => x.IsDeny).Select(x => x.Permission).ToList())
            {
                user.Permissions.RemoveAll(x => x.Equals(denyPermission));
            }

            //Permission keylerin tekilligi kontrol edilir.
            user.Permissions = user.Permissions.Distinct().ToList();

            return user;
        }



        public void RemoveRefreshToken(string userName)
        {
            base.UpdateWithTemplate("AuthenticationDAO.RemoveRefreshToken", new { Username = userName });
        }

        public void UpdateRefreshToken(string userName, string refreshToken)
        {
            base.UpdateWithTemplate("AuthenticationDAO.UpdateRefreshToken", new { RefreshToken = refreshToken, Username = userName });
        }

        public void SetRefreshToken(string userName, string refreshToken, DateTime refreshtokencreatedate)
        {
            base.UpdateWithTemplate("AuthenticationDAO.SetRefreshToken", new { RefreshToken = refreshToken, RefreshTokenCreateDate = refreshtokencreatedate, Username = userName });
        }

        public LoginDAOModel GetLoginUserByRefreshToken(string refreshToken, string userName)
        {

            LoginDAOModel user = base.SelectWithTemplate<LoginDAOModel>("AuthenticationDAO.GetLoginUserByRefreshToken", new { RefreshToken = refreshToken, Username = userName }).FirstOrDefault();

            if (user == null)
                return null;

            List<LoginRoleDAOModel> userRoles = base.SelectWithTemplate<LoginRoleDAOModel>("AuthenticationDAO.GetloginUserRoles", new { UserId = user.Id }).ToList();

            if (userRoles.Count > 0)
                user.Roles = userRoles.Select(x => x.Role).ToList();

            List<LoginPermissionDAOModel> userPermissions = base.SelectWithTemplate<LoginPermissionDAOModel>("AuthenticationDAO.GetloginUserPermissions", new { UserId = user.Id }).ToList();
            List<LoginRolePermissionDAOModel> rolePermissions = base.SelectWithTemplate<LoginRolePermissionDAOModel>("AuthenticationDAO.GetloginUserRolePermissions", new { UserId = user.Id }).ToList();

            user.Permissions = new List<string>();

            if (rolePermissions.Count > 0)
            {
                user.Permissions.AddRange(rolePermissions.Select(x => x.Permission).ToList());
            }

            //Rolden gelen yetkilere ek olarak kullanıcı üzerinde verilen yetkiler eklenir.
            //IsDeny=true olanlar ise yetkilerden çıkarılır

            if (userPermissions.Where(x => !x.IsDeny).ToList().Count > 0)
                user.Permissions.AddRange(userPermissions.Where(x => !x.IsDeny).Select(x => x.Permission).ToList());


            foreach (var denyPermission in userPermissions.Where(x => x.IsDeny).Select(x => x.Permission).ToList())
            {
                user.Permissions.RemoveAll(x => x.Equals(denyPermission));
            }

            //Permission keylerin tekilligi kontrol edilir.
            user.Permissions = user.Permissions.Distinct().ToList();

            return user;

        }

        public LoginUserFailedDAOModel LoginUserFailed(string userName)
        {
            LoginUserFailedDAOModel loginFailed = base.SelectWithTemplate<LoginUserFailedDAOModel>("AuthenticationDAO.LoginUserFailed", new { Username = userName }).FirstOrDefault();

            return loginFailed;
        }

        public void UpdateLoginFailed(UpdateLoginFailedDAOModel dAOModel)
        {
            base.UpdateWithTemplate("AuthenticationDAO.UpdateLoginFailed", dAOModel);
        }

     

    }
}
