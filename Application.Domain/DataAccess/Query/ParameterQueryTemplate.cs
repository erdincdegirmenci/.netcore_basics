using DefineXwork.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.DataAccess.Query
{
    public class ParameterQueryTemplate : IQueryTemplate
    {
        readonly Dictionary<string, string> _queries = new Dictionary<string, string>();

        public ParameterQueryTemplate()
        {
            _queries.Add("ParameterDAO.GetAllParameters", @"Select Id,Code,Value,Group,Type,IsActive,Order from tblParameter;");
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
