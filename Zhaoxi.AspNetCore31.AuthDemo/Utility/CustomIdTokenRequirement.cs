using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore31.AuthDemo.Utility
{

    public class CustomIdTokenRequirement : IAuthorizationRequirement
    {

    }

    public class CustomIdTokenHandler : AuthorizationHandler<CustomIdTokenRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomIdTokenRequirement requirement)
        {
            //if (context.User != null && context.User.HasClaim(c => c.Type == ClaimTypes.Email))
            //{
            //    var email = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
            if (context.User != null && context.User.HasClaim(c => c.Type == "EMail"))
            {
                var email = context.User.FindFirst(c => c.Type == "EMail").Value;
                Console.WriteLine(email);
                if (email.EndsWith("@qq.com", StringComparison.OrdinalIgnoreCase))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //context.Fail();//不设置失败
                }
            }
            return Task.CompletedTask;
        }
    }
}
