using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore31.AuthDemo.Utility
{
    /// <summary>
    /// QQ邮箱
    /// </summary>
    public class QQEmailRequirement : AuthorizationHandler<NameAuthorizationRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NameAuthorizationRequirement requirement)
        {
            if (context.User != null && context.User.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                var email = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
                if (email.EndsWith("@qq.com", StringComparison.OrdinalIgnoreCase))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //context.Fail();//没成功就留给别人处理
                }
            }
            return Task.CompletedTask;
        }
    }


    public class CustomRequirement : AuthorizationHandler<NameAuthorizationRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NameAuthorizationRequirement requirement)
        {
            if (context.User != null && context.User.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                var email = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;
                if (email.EndsWith("@qq.com", StringComparison.OrdinalIgnoreCase))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //context.Fail();//没成功就留给别人处理
                }
            }
            return Task.CompletedTask;
        }
    }
}
