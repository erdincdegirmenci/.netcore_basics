using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.DataAccess.Query
{
    public class UserVerificationQueryTemplate : IQueryTemplate
    {
        readonly Dictionary<string, string> _queries = new Dictionary<string, string>();

        public UserVerificationQueryTemplate()
        {
            _queries.Add("UserVerificationDAO.GetUserVerification", @"Select Id,UserId,VerificationCode,VerificationDate,ExpireTime,VerificationType,IsVerificate from tblUserVerification where UserId=@UserId;");
            _queries.Add("UserVerificationDAO.CreateUserVerification", @"Insert Into tblUserVerification (UserId,VerificationCode,ExpireTime,VerificationType,IsVerificate,CreateDate,CreateUser) Values (@UserId,@VerificationCode,@ExpireTime,@VerificationType,@IsVerificate,@CreateDate,@CreateUser); Select LAST_INSERT_ID();");
            _queries.Add("UserVerificationDAO.UpdateUserVerification", @"Update tblUserVerification set VerificationDate=@VerificationDate, IsVerificate=@IsVerificate,ExpireTime=null,LastUpdateDate=@LastUpdateDate,LastUpdateUser=@LastUpdateUser where UserId=@UserId and Id=@Id");
            _queries.Add("UserVerificationDAO.GetUserVerificationByVericationCode", @"Select Id,UserId,VerificationCode,VerificationDate,ExpireTime,VerificationType,IsVerificate from tblUserVerification where VerificationCode=@VerificationCode;");

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
