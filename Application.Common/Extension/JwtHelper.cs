using DefineXwork.Library.Configuration;
using DefineXwork.Library.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Extension
{
    public class JwtHelper
    {
        private readonly IConfigManager _configManager;

        public JwtHelper(IConfigManager configManager)
        {
            _configManager = configManager;
        }
        public JwtOptions GetJwtOptions()
        {

            return new JwtOptions()
            {
                Audience = _configManager.GetConfig("JwtOptions:Audience"),
                Issuer = _configManager.GetConfig("JwtOptions:Issuer"),
                TokenExpiration = _configManager.GetConfig("JwtOptions:TokenExpiration").ToInt(),
                SecurityKey = _configManager.GetConfig("JwtOptions:SecurityKey"),
                RefreshTokenExpiration = _configManager.GetConfig("JwtOptions:RefreshTokenExpiration").ToInt()
            };
        }

        public JwtOptions GetJwtOptions(int tokenExpiration)
        {
            return new JwtOptions()
            {
                Audience = _configManager.GetConfig("JwtOptions:Audience"),
                Issuer = _configManager.GetConfig("JwtOptions:Issuer"),
                TokenExpiration = tokenExpiration,
                SecurityKey = _configManager.GetConfig("JwtOptions:SecurityKey"),
                RefreshTokenExpiration = _configManager.GetConfig("JwtOptions:RefreshTokenExpiration").ToInt()
            };
        }
    }
}
