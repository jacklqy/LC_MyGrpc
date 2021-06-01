using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.AspNetCore31.AuthenticationCenter.Utility.RSA;

namespace Zhaoxi.AspNetCore31.AuthenticationCenter.Utility
{

    public class JWTRSService : IJWTService
    {
        #region Option注入
        private readonly JWTTokenOptions _JWTTokenOptions;
        public JWTRSService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            this._JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }
        #endregion



        public string GetToken(CurrentUserModel userModel)
        {
            string jtiCustom = Guid.NewGuid().ToString();//用来标识 Token
            var claims = new[]
            {
                   new Claim(ClaimTypes.Name, userModel.Name),

                   new Claim("jti", jtiCustom, ClaimValueTypes.String),

                   new Claim("EMail", userModel.EMail),
                   new Claim("Account", userModel.Account),
                   new Claim("Age", userModel.Age.ToString()),
                   new Claim("Id", userModel.Id.ToString()),
                   new Claim("Mobile", userModel.Mobile),
                   new Claim(ClaimTypes.Role,userModel.Role),
                   //new Claim("Role", userModel.Role),//这个不能角色授权
                   new Claim("Sex", userModel.Sex.ToString())//各种信息拼装
            };

            string keyDir = Directory.GetCurrentDirectory();
            if (RSAHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams) == false)
            {
                keyParams = RSAHelper.GenerateAndSaveKey(keyDir);
            }
            var credentials = new SigningCredentials(new RsaSecurityKey(keyParams), SecurityAlgorithms.RsaSha256Signature);

            #region XML
            //string privateKey = RSAHelper.GenerateAndSaveKey(keyDir);
            //var  RSA = new RSACryptoServiceProvider();
            //RSA.FromXmlString(privateKey);
            //var credentials = new SigningCredentials(new RsaSecurityKey(RSA), SecurityAlgorithms.RsaSha256Signature);
            #endregion

            var token = new JwtSecurityToken(
               issuer: this._JWTTokenOptions.Issuer,
               audience: this._JWTTokenOptions.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(60),//5分钟有效期
               signingCredentials: credentials);
            var handler = new JwtSecurityTokenHandler();
            string tokenString = handler.WriteToken(token);
            return tokenString;
        }
    }
}
