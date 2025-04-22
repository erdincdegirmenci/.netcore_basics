using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Domain.DataAccess.Query
{
    public class UserQueryTemplate : IQueryTemplate
    {
        readonly Dictionary<string, string> _queries = new Dictionary<string, string>();

        public UserQueryTemplate()
        {
            _queries.Add("UserDAO.GetUserByUsername", @"Select Id,Username,Name,Surname,UserType,IsActive from tblUser where Username=@Username and IsDeleted=0");
            _queries.Add("UserDAO.GetActiveUserByUsername", @"Select Id,Username,Name,Surname,UserType,IsActive from tblUser where Username=@Username and IsActive=1 and IsDeleted=0");
            _queries.Add("UserDAO.SetUserStatus", @"Update tblUser Set IsActive=@IsActive Where Username=@Username");
            _queries.Add("UserDAO.CreateUser", @"Insert Into tblUser (Username,Password,Name,Surname,UserType,IsActive,CreateDate,CreateUser) Values (@Username,@Password,@Name,@Surname,@UserType,@IsActive,@CreateDate,@CreateUser); Select LAST_INSERT_ID();");
            _queries.Add("UserDAO.GetUserById", @"Select Id,Username,Name,Surname,UserType,IsActive from tblUser where Id=@Id and IsDeleted=0");
            _queries.Add("UserDAO.UpdateUserPassword", @"Update tblUser set Password=@Password where Id=@Id");
           
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
