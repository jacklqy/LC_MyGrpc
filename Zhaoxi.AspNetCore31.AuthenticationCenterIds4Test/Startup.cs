using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zhaoxi.AspNetCore31.AuthenticationCenterIds4Test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public class ClientInitConfig
        {
            /// <summary>
            /// 定义ApiResource   
            /// 这里的资源（Resources）指的就是管理的API
            /// </summary>
            /// <returns>多个ApiResource</returns>
            public static IEnumerable<ApiResource> GetApiResources()
            {
                return new[]
                {
                new ApiResource("UserApi", "用户获取API")
            };
            }

            /// <summary>
            /// 定义验证条件的Client
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<Client> GetClients()
            {
                return new[]
                {
                new Client
                {
                    ClientId = "Zhaoxi.AspNetCore31.AuthDemo",//客户端惟一标识
                    ClientSecrets = new [] { new Secret("eleven123456".Sha256()) },//客户端密码，进行了加密
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //授权方式，客户端认证，只要ClientId+ClientSecrets
                    AllowedScopes = new [] { "UserApi" },//允许访问的资源
                    Claims=new List<Claim>(){
                        new Claim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                        new Claim(IdentityModel.JwtClaimTypes.NickName,"Eleven"),
                        new Claim("eMail","57265177@qq.com")
                    }
                }
            };
            }
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            services.AddControllersWithViews();

            #region 客户端
            services.AddIdentityServer()
              .AddDeveloperSigningCredential()//默认的开发者证书 
              .AddInMemoryClients(ClientInitConfig.GetClients())
              .AddInMemoryApiResources(ClientInitConfig.GetApiResources());
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
           app.UseHttpsRedirection();
            app.UseStaticFiles();

            #region 添加IdentityServer中间件
            app.UseIdentityServer();
            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
