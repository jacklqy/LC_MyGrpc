using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore31.AuthenticationCenterIds4.DataInit.DB
{
    /// <summary>
    /// 自定义用户检测方法---完成数据库校验
    /// </summary>
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserServiceTest _iUsersServices;
        public CustomResourceOwnerPasswordValidator(IUserServiceTest userService)
        {
            _iUsersServices = userService;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            Console.WriteLine($"This is CustomResourceOwnerPasswordValidator {context.UserName}--{context.Password}");
            var user = this._iUsersServices.Login(context.UserName, context.UserName);//正常数据库
            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            }
            else
            {
                context.Result = new GrantValidationResult(
                        user.UId.ToString(),
                        OidcConstants.AuthenticationMethods.Password,
                        DateTime.UtcNow,
                        user.Claims);
            }
            return Task.CompletedTask;
        }
    }

    public interface IUserServiceTest
    {
        UserTestDTO Login(string userName, string password);
    }
    public class UserServiceTest : IUserServiceTest
    {
        public UserTestDTO Login(string userName, string password)
        {
            return new UserTestDTO()
            {
                UId = 123,
                UserName = userName,
                Password = password,
                Claims = new List<Claim>(){
                        new Claim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                        new Claim(IdentityModel.JwtClaimTypes.NickName,"Eleven"),
                        new Claim("eMail","57265177@qq.com")
                    }
            };
        }
    }
    public class UserTestDTO
    {
        public int UId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
