using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Zhaoxi.AspNetCore31.AuthDemo.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string name, string password)
        {
            //base.HttpContext.RequestServices.
            //IAuthenticationService

            if ("Eleven".Equals(name, StringComparison.CurrentCultureIgnoreCase)
                && password.Equals("123456"))
            {
                var claimIdentity = new ClaimsIdentity("Custom");
                claimIdentity.AddClaim(new Claim(ClaimTypes.Name, name));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Email, "xuyang@ZhaoxiEdu.Net"));
                //claimIdentity.IsAuthenticated = true;
                await base.HttpContext.SignInAsync("CustomScheme", new ClaimsPrincipal(claimIdentity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                });
                return new JsonResult(new
                {
                    Result = true,
                    Message = "登录成功"
                });
            }
            else
            {
                await Task.CompletedTask;
                return new JsonResult(new
                {
                    Result = false,
                    Message = "登录失败"
                });
            }
        }

        public async Task<IActionResult> Logout()
        {
            await base.HttpContext.SignOutAsync("CustomScheme");
            return new JsonResult(new
            {
                Result = true,
                Message = "退出成功"
            });
        }

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IActionResult> Authentication()
        {
            var result = await base.HttpContext.AuthenticateAsync("CustomScheme");
            if (result?.Principal != null)
            {
                base.HttpContext.User = result.Principal;
                return new JsonResult(new
                {
                    Result = true,
                    Message = $"认证成功，包含用户{base.HttpContext.User.Identity.Name}"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    Result = true,
                    Message = $"认证失败，用户未登录"
                });
            }
        }
        /// <summary>
        /// 授权
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Authorization()
        {
            var result = await base.HttpContext.AuthenticateAsync("CustomScheme");
            if (result?.Principal == null)
            {
                return new JsonResult(new
                {
                    Result = true,
                    Message = $"认证失败，用户未登录"
                });
            }
            else
            {
                base.HttpContext.User = result.Principal;
            }

            //授权
            var user = base.HttpContext.User;
            if (user?.Identity?.IsAuthenticated ?? false)
            {
                if (!user.Identity.Name.Equals("Eleven", StringComparison.OrdinalIgnoreCase))
                {
                    await base.HttpContext.ForbidAsync("CustomScheme");
                    return new JsonResult(new
                    {
                        Result = false,
                        Message = $"授权失败，用户{base.HttpContext.User.Identity.Name}没有权限"
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        Result = false,
                        Message = $"授权成功，用户{base.HttpContext.User.Identity.Name}具备权限"
                    });
                }
            }
            else
            {
                await base.HttpContext.ChallengeAsync("CustomScheme");
                return new JsonResult(new
                {
                    Result = false,
                    Message = $"授权失败，没有登录"
                });
            }
        }


        /// <summary>
        /// 需要授权的页面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Info()
        {
            var result = await base.HttpContext.AuthenticateAsync("CustomScheme");
            if (result?.Principal == null)
            {
                return new JsonResult(new
                {
                    Result = true,
                    Message = $"认证失败，用户未登录"
                });
            }
            else
            {
                base.HttpContext.User = result.Principal;
            }

            //授权
            var user = base.HttpContext.User;
            if (user?.Identity?.IsAuthenticated ?? false)
            {
                if (!user.Identity.Name.Equals("Eleven", StringComparison.OrdinalIgnoreCase))
                {
                    await base.HttpContext.ForbidAsync("CustomScheme");
                    return new JsonResult(new
                    {
                        Result = false,
                        Message = $"授权失败，用户{base.HttpContext.User.Identity.Name}没有权限"
                    });
                }
                else
                {
                    //有权限
                    return new JsonResult(new
                    {
                        Result = true,
                        Message = $"授权成功，正常访问页面！",
                        Html = "Hello Root!"
                    });
                }
            }
            else
            {
                await base.HttpContext.ChallengeAsync("CustomScheme");
                return new JsonResult(new
                {
                    Result = false,
                    Message = $"授权失败，没有登录"
                });
            }
        }

    }
}