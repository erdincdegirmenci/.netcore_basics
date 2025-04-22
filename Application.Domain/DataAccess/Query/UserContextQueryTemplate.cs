using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Domain.DataAccess.Query
{
    public class UserContextQueryTemplate: IQueryTemplate
    {
        readonly Dictionary<string, string> _queries = new Dictionary<string, string>();
        public UserContextQueryTemplate()
        {
            _queries.Add("UserContextDAO.GetUser", @"Select Id,Username,Name,Surname,UserType,IsActive from tblUser where Username=@Username and IsActive=1 and IsDeleted=0");
            _queries.Add("UserContextDAO.GetUserRoles", @"Select RoleId,Name as Role from tblUserRole ur inner join tblRole r on r.Id=ur.RoleId where UserId=@UserId and r.IsActive=1");
            _queries.Add("UserContextDAO.GetUserPermissions", @"Select PermissionId,Name as Permission ,IsDeny from tblUserPermission up inner join tblPermission p on p.Id=up.PermissionId where UserId=@UserId and p.IsActive=1");
            _queries.Add("UserContextDAO.GetAuthenticatedUser", @"Select Id,Username,Name,Surname,UserType,IsActive from tblUser where Username=@Username and Password=@Password and IsDeleted=0 and IsActive=1;");
            _queries.Add("UserContextDAO.GetRolePermissions", @"Select PermissionId,Name as Permission from tblRolePermission rp inner join tblPermission p on p.Id=rp.PermissionId inner join tblUserRole ur on ur.RoleId=rp.RoleId where ur.UserId=@UserId");


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
