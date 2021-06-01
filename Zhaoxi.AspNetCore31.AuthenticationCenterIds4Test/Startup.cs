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
            /// ����ApiResource   
            /// �������Դ��Resources��ָ�ľ��ǹ����API
            /// </summary>
            /// <returns>���ApiResource</returns>
            public static IEnumerable<ApiResource> GetApiResources()
            {
                return new[]
                {
                new ApiResource("UserApi", "�û���ȡAPI")
            };
            }

            /// <summary>
            /// ������֤������Client
            /// </summary>
            /// <returns></returns>
            public static IEnumerable<Client> GetClients()
            {
                return new[]
                {
                new Client
                {
                    ClientId = "Zhaoxi.AspNetCore31.AuthDemo",//�ͻ���Ωһ��ʶ
                    ClientSecrets = new [] { new Secret("eleven123456".Sha256()) },//�ͻ������룬�����˼���
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    //��Ȩ��ʽ���ͻ�����֤��ֻҪClientId+ClientSecrets
                    AllowedScopes = new [] { "UserApi" },//������ʵ���Դ
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

            #region �ͻ���
            services.AddIdentityServer()
              .AddDeveloperSigningCredential()//Ĭ�ϵĿ�����֤�� 
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

            #region ���IdentityServer�м��
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
