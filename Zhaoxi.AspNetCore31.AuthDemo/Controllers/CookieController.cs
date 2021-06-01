using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Zhaoxi.AspNetCore31.AuthDemo.Controllers
{
    [Authorize]
    public class CookieController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string name, string password)
        {
            if ("Eleven".Equals(name, StringComparison.CurrentCultureIgnoreCase)
                && password.Equals("123456"))
            {
                //var claimIdentity = new ClaimsIdentity("Cookie");
                //claimIdentity.AddClaim(new Claim(ClaimTypes.Name, name));
                //claimIdentity.AddClaim(new Claim(ClaimTypes.Email, "xuyang@ZhaoxiEdu.Net"));

                #region 升级JwtClaimTypes  cookie会短一点
                var claimIdentity = new ClaimsIdentity("Cookie", JwtClaimTypes.Name, JwtClaimTypes.Role);
                claimIdentity.AddClaim(new Claim(JwtClaimTypes.Name, name));
                claimIdentity.AddClaim(new Claim(JwtClaimTypes.Email, "xuyang@ZhaoxiEdu.Net"));
                #endregion

                await base.HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                });//省略scheme
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
        /// <summary>
        /// 授权
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AuthenticationAuthorization()
        {
            var result = await base.HttpContext.AuthenticateAsync();//默认CookieAuthenticationDefaults.AuthenticationScheme
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
                    await base.HttpContext.ForbidAsync();
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
                await base.HttpContext.ChallengeAsync();
                return new JsonResult(new
                {
                    Result = false,
                    Message = $"授权失败，没有登录"
                });
            }
        }

        public async Task<IActionResult> Logout()
        {
            await base.HttpContext.SignOutAsync();
            return new JsonResult(new
            {
                Result = true,
                Message = "退出成功"
            });
        }

        public IActionResult Info()
        {
            return View();
        }
    }
}