using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zhaoxi.AspNetCore31.AuthenticationCenterIds4.DataInit.DB;

namespace Zhaoxi.AspNetCore31.AuthenticationCenterIds4.Utility
{
    /// <summary>
    /// 扩展-类型-校验器
    /// </summary>
    public class CustomElevenGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => "CustomEleven";

        private readonly IUserServiceTest _iUserServiceTest;

        public CustomElevenGrantValidator(IUserServiceTest userServiceTest)
        {
            this._iUserServiceTest = userServiceTest;
        }

        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var CE_name = context.Request.Raw.Get("CE_name");
            var CE_password = context.Request.Raw.Get("CE_password");
            Console.WriteLine($"This is CustomElevenGrantValidator CE_name={CE_name}--CE_password={CE_password}");

            if (string.IsNullOrEmpty(CE_name) || string.IsNullOrEmpty(CE_password))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            }
            var result = this._iUserServiceTest.Login(CE_name, CE_password);
            if (result == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            }
            else
            {
                Console.WriteLine($"This is CustomElevenGrantValidator CE_name={CE_name}--CE_password={CE_password}");
                context.Result = new GrantValidationResult(
                             subject: result.UId.ToString(),
                             authenticationMethod: GrantType,
                             claims: result.Claims);
            }
            return Task.CompletedTask;
        }
    }
}
