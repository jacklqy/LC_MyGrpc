using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore31.AuthenticationCenterIds4.DataInit
{
    /// <summary>
    /// 授权码模式：用户---朝夕---腾讯授权中心
    /// http://localhost:7200/connect/authorize?client_id=Zhaoxi.AspNetCore31.AuthDemo&redirect_uri=http://localhost:5726/Ids4/IndexCodeToken&response_type=code&scope=UserApi
    /// 用户访问朝夕---需要token---跳转到授权中心---朝夕提供地址---然后用户向腾讯授权中心输入账号密码
    /// --返回Code--拿着Code+clientpassword通过后端去获取token
    /// 
    /// f9iE2qzai38_dHvA8nef-zN0IH3OrbKY9Ut6Twar2R0
    /// 
    /// </summary>
    public class CodeInitConfig
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
                //new ApiResource("UserApi", "用户获取API"),
                // new ApiResource("TestApi", "用户TestAPI")
                new ApiResource("UserApi", "用户获取API",new List<string>(){IdentityModel.JwtClaimTypes.Role,"eMail" }),
                 new ApiResource("TestApi", "用户TestAPI",new List<string>(){IdentityModel.JwtClaimTypes.Role,"eMail" })
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser()
                {
                     Username="Eleven",
                     Password="123456",
                     SubjectId="0",
                     Claims=new List<Claim>(){
                        new Claim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                        new Claim(IdentityModel.JwtClaimTypes.NickName,"Eleven"),
                        new Claim("eMail","57265177@qq.com")
                    }
                }
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
                    ClientName="ApiClient for Code",
                    ClientSecrets = new [] { new Secret("eleven123456".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,//授权码
                    RedirectUris={"http://localhost:5726/Ids4/IndexCodeToken" },//可以多个
                    AllowedScopes = new [] { "UserApi","TestApi" },//允许访问的资源
                    AllowAccessTokensViaBrowser=true//允许将token通过浏览器传递
                }
            };
        }
    }
}


