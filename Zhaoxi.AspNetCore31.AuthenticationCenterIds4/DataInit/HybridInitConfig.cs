using IdentityServer4;
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
    /// 混合模式:access_token+id_token
    /// http://localhost:7200/connect/authorize?client_id=Zhaoxi.AspNetCore31.AuthDemo&redirect_uri=http://localhost:5726/Ids4/IndexCodeToken&response_type=code%20token%20id_token&scope=UserApi%20openid%20CustomIdentityResource&response_model=fragment&nonce=12345

    /// 
    /// 4种模式：code,code%20token,code%20id_token,code%20token%20id_token
    /// 获取tokenid，scope必须%20openid，其他随意
    /// 必须加入nonce
    /// 自定义claim需要从IdentityResource增加，然后allowscope允许
    /// 
    /// http://localhost:5000/connect/authorize?client_id=apiClientImpl&redirect_uri=https://localhost:5002/auth.html&response_type=token&scope=secretapi
    /// </summary>
    public class HybridInitConfig
    {
        /// <summary>
        /// 用户信息，能返回哪些用户信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
               new IdentityResources.OpenId(),
               new IdentityResources.Profile(),//一堆默认属性
               new IdentityResource(
                   "CustomIdentityResource",
                   "This is Custom Model",
                    new List<string>(){ "phonemodel","phoneprise", "eMail"})//自定义Id资源，植入claim

            };
        }

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
                        new Claim(ClaimTypes.Name,"apiUser"),
                        new Claim("eMail","57265177@qq.com"),
                        new Claim("prog","正式项目"),
                        new Claim("phonemodel","huawei"),
                        new Claim("phoneprise","5000元"),
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
                    AlwaysIncludeUserClaimsInIdToken=true,
                    AllowOfflineAccess = true,

                    ClientId = "Zhaoxi.AspNetCore31.AuthDemo",//客户端惟一标识
                    ClientName="ApiClient for HyBrid",
                    ClientSecrets = new [] { new Secret("eleven123456".Sha256()) },
                    AccessTokenLifetime=3600,//默认1小时
                    AllowedGrantTypes = GrantTypes.Hybrid,//混合模式
                    RedirectUris={"http://localhost:5726/Ids4/IndexCodeToken" },//可以多个
                    AllowedScopes = new [] {
                        "UserApi",
                        "TestApi",//资源范围
                        IdentityServerConstants.StandardScopes.OpenId,//Ids4：获取Id_token，必需加入"openid"
                         IdentityServerConstants.StandardScopes.Profile,//用户信息范围
                       "CustomIdentityResource"},
                    AllowAccessTokensViaBrowser=true//允许将token通过浏览器传递
                }
            };
        }
    }
}


