using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Domain.DataAccess.Query
{
    public class AuthenticationQueryTemplate : IQueryTemplate
    {
        readonly Dictionary<string, string> _queries = new Dictionary<string, string>();
        
        public AuthenticationQueryTemplate()
        {
            _queries.Add("AuthenticationDAO.GetLoginUser", @"Select Id, Username,TenantId,RefreshToken,RefreshTokenCreateDate,IsActive,LoginFailedCount,LastLoginFailedDate from tblUser where Username=@Username and Password=@Password and IsDeleted=0;");
            _queries.Add("AuthenticationDAO.TestInsert", @"Insert Into tblUser (Username,Password,Name,Surname) Values (@Username,@Password,@Name,@Surname); Select LAST_INSERT_ID();");
            _queries.Add("AuthenticationDAO.GetloginUserRoles", @"Select RoleId,Name as Role from tblUserRole ur inner join tblRole r on r.Id=ur.RoleId where UserId=@UserId and r.IsActive=1");
            _queries.Add("AuthenticationDAO.GetLoginUserByRefreshToken", @"Select Id,Username,RefreshToken,RefreshTokenCreateDate,IsActive from tblUser where RefreshToken=@RefreshToken and Username=@Username and IsActive=1 and IsDeleted=0");
            _queries.Add("AuthenticationDAO.SetRefreshToken", @"Update tblUser set RefreshToken=@RefreshToken,RefreshTokenCreateDate=@RefreshTokenCreateDate where Username=@Username");
            _queries.Add("AuthenticationDAO.UpdateRefreshToken", @"Update tblUser set RefreshToken=@RefreshToken where Username=@Username");
            _queries.Add("AuthenticationDAO.RemoveRefreshToken", @"Update tblUser set RefreshToken='' where Username=@Username");
            _queries.Add("AuthenticationDAO.LoginUserFailed", @"Select LoginFailedCount,LastLoginFailedDate,IsActive from tblUser Where Username=@Username and IsDeleted=0");
            _queries.Add("AuthenticationDAO.UpdateLoginFailed", @"Update tblUser set LoginFailedCount=@LoginFailedCount, LastLoginFailedDate=@LastLoginFailedDate Where Username=@Username");
            _queries.Add("AuthenticationDAO.GetloginUserPermissions", @"Select PermissionId,Name as Permission,IsDeny from tblUserPermission up inner join tblPermission p on p.Id=up.PermissionId where UserId=@UserId and p.IsActive=1");
            _queries.Add("AuthenticationDAO.GetloginUserRolePermissions", @"Select PermissionId,Name as Permission from tblRolePermission rp inner join tblPermission p on p.Id=rp.PermissionId inner join tblUserRole ur on ur.RoleId=rp.RoleId where ur.UserId=@UserId");

        }
        public string GetQuery(string key)
        {
            if (!_queries.TryGetValue(key, out string value))
                throw new Exception("Query is not found. Query Key : " + key);
            return value;
        }

        public string GetQuery(string key, string dynamicWhereClause)
        {
            return GetQuery(key).Replace("[DynamicWhereClause]", $"where {dynamicWhereClause}");
        }
    }
}
