using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Zhaoxi.AspNetCore31.AuthDemo.Controllers
{
    //[Authorize(AuthenticationSchemes = "Cookie", Roles = "Admin,User", Policy = "Custom,Eleven")]

    /// <summary>
    /// VS直接启动调试 5001端口成功
    /// 脚本命令行5821失败
    /// </summary>
    public class AuthorizationController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string name, string password, string role = "Admin")
        {
            //base.HttpContext.RequestServices.
            //IAuthenticationService

            if ("Eleven".Equals(name, StringComparison.CurrentCultureIgnoreCase)
                && password.Equals("123456"))
            {
                var claimIdentity = new ClaimsIdentity("Custom");
                claimIdentity.AddClaim(new Claim(ClaimTypes.Name, name));
                //claimIdentity.AddClaim(new Claim(ClaimTypes.Email, "xuyang@ZhaoxiEdu.Net"));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Email, "57265177@qq.com"));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

                await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                });//登录为默认的scheme  cookies
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
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new JsonResult(new
            {
                Result = true,
                Message = "退出成功"
            });
        }

        /// <summary>
        /// 需要授权的页面
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Cookies")]//
        public IActionResult Info()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult InfoAdmin()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult InfoUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult InfoAdminUser()
        {
            return View();
        }

        #region Policy
        [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminPolicy")]
        public IActionResult InfoAdminPolicy()
        {
            return View();
        }
        [Authorize(AuthenticationSchemes = "Cookies", Policy = "UserPolicy")]
        public IActionResult InfoUserPolicy()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "Cookies", Policy = "QQEmail")]
        public IActionResult InfoQQEmail()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "Cookies", Policy = "DoubleEmail")]
        public IActionResult InfoDoubleEmail()
        {
            return View();
        }
        #endregion


        #region CustomScheme
        [AllowAnonymous]
        public async Task<IActionResult> LoginCustomScheme(string name, string password)
        {
            //base.HttpContext.RequestServices.
            //IAuthenticationService

            if ("ElevenCustomScheme".Equals(name, StringComparison.CurrentCultureIgnoreCase)
                && password.Equals("123456"))
            {
                var claimIdentity = new ClaimsIdentity("Custom");
                claimIdentity.AddClaim(new Claim(ClaimTypes.Name, name));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Email, "xuyang@ZhaoxiEdu.Net"));
                await base.HttpContext.SignInAsync("CustomScheme", new ClaimsPrincipal(claimIdentity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                });//登录为默认的scheme  cookies
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

        public async Task<IActionResult> LogoutCustomScheme()
        {
            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new JsonResult(new
            {
                Result = true,
                Message = "退出成功"
            });
        }

        /// <summary>
        /// 需要授权的页面
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "CustomScheme")]
        public IActionResult InfoCustomScheme()
        {
            return View();
        }
        #endregion
    }
}