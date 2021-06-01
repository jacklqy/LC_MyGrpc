using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Logging;
using Zhaoxi.AspNetCore31.AuthDemo.Models;
using Zhaoxi.AspNetCore31.AuthDemo.Utility;

namespace Zhaoxi.AspNetCore31.AuthDemo.Controllers
{
    [CustomAuthorizationFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string name, string password)
        {
            if ("Eleven".Equals(name, StringComparison.CurrentCultureIgnoreCase)
                 && password.Equals("123456"))
            {
                #region Filter
                base.HttpContext.Response.Cookies.Append("CurrentUser", "Eleven", new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Expires = DateTime.UtcNow.AddMinutes(30)
                });
                #endregion

                return new JsonResult(new
                {
                    Result = true,
                    Message = "登录成功"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    Result = false,
                    Message = "登录失败"
                });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
