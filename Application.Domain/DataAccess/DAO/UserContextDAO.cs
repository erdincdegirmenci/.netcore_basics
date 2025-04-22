using Application.Domain.DataAccess.DAO.Interface;
using Application.Domain.DataAccess.Model.UserContext;
using DefineXwork.Library.DataAccess;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Application.Domain.DataAccess.DAO
{
    public class UserContextDAO : BaseDAO<IDatabaseManager>, IUserContextDAO
    {

        // Sadece frameworkten gelen JWT authenticatin sonrası user bilgisini almak için kullanılır

        public UserContextDAO(IDatabaseManager databaseManager) : base(databaseManager)
        {

        }
        public UserContextDAO(IDatabaseManager databaseManager, IQueryTemplate queryTemplate) : base(databaseManager, queryTemplate)
        {

        }

        public UserContextUserDAOModel GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;
            UserContextUserDAOModel user = base.SelectWithTemplate<UserContextUserDAOModel>("UserContextDAO.GetUser", new { Username = userName }).FirstOrDefault();
            if (user == null)
                return null;
            List<UserContextUserRoleDAOModel> userRoles = base.SelectWithTemplate<UserContextUserRoleDAOModel>("UserContextDAO.GetUserRoles", new { UserId = user.Id }).ToList();
            if (userRoles.Count > 0)
                user.Roles = userRoles.Select(x => x.Role).ToList();

            List<UserContextUserPermissionDAOModel> userPermissions = base.SelectWithTemplate<UserContextUserPermissionDAOModel>("UserContextDAO.GetUserPermissions", new { UserId = user.Id }).ToList();
            List<UserContextRolePermissionDAOModel> rolePermissions = base.SelectWithTemplate<UserContextRolePermissionDAOModel>("UserContextDAO.GetRolePermissions", new { UserId = user.Id }).ToList();

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

        public UserContextUserDAOModel GetAuthenticatedUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;
            UserContextUserDAOModel user = base.SelectWithTemplate<UserContextUserDAOModel>("UserContextDAO.GetAuthenticatedUser", new { Username = username, Password = password }).FirstOrDefault();
            if (user == null)
                return null;
            List<UserContextUserRoleDAOModel> userRoles = base.SelectWithTemplate<UserContextUserRoleDAOModel>("UserContextDAO.GetUserRoles", new { UserId = user.Id }).ToList();
            if (userRoles.Count > 0)
                user.Roles = userRoles.Select(x => x.Role).ToList();

            List<UserContextUserPermissionDAOModel> userPermissions = base.SelectWithTemplate<UserContextUserPermissionDAOModel>("UserContextDAO.GetUserPermissions", new { UserId = user.Id }).ToList();
            List<UserContextRolePermissionDAOModel> rolePermissions = base.SelectWithTemplate<UserContextRolePermissionDAOModel>("UserContextDAO.GetRolePermissions", new { UserId = user.Id }).ToList();

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
    }

}

