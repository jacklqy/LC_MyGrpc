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
    /// 隐藏模式：用户---朝夕---腾讯授权中心
    /// http://localhost:7200/connect/authorize?client_id=Zhaoxi.AspNetCore31.AuthDemo&redirect_uri=http://localhost:5726/Ids4/IndexToken&response_type=token&scope=UserApi
    /// 用户访问朝夕---需要token---跳转到授权中心---朝夕提供地址---然后用户向腾讯授权中心输入账号密码
    /// 
    /// http://localhost:5726/Ids4/IndexToken#access_token=eyJhbGciOiJSUzI1NiIsImtpZCI6Imc0TlhXdm9YMTFJZ21ybUNScHR5aFEiLCJ0eXAiOiJhdCtqd3QifQ.eyJuYmYiOjE1OTAxNTUwMjUsImV4cCI6MTU5MDE1ODYyNSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo3MjAwIiwiYXVkIjoiVXNlckFwaSIsImNsaWVudF9pZCI6IlpoYW94aS5Bc3BOZXRDb3JlMzEuQXV0aERlbW8iLCJzdWIiOiIwIiwiYXV0aF90aW1lIjoxNTkwMTU0OTQ3LCJpZHAiOiJsb2NhbCIsInJvbGUiOiJBZG1pbiIsImVNYWlsIjoiNTcyNjUxNzdAcXEuY29tIiwic2NvcGUiOlsiVXNlckFwaSJdLCJhbXIiOlsicHdkIl19.WdeNz6A2AxPCU8iO7X6D5ewQD2rVn72CZz7Z_bKUuQU5rdMn3IfIUsKioTzOf5UCrf4oNicu2smYR2VGt7eCFB1_OYAgFWxMDPVu0iVDuTOj8Uhuxwdy9fdPfHCcAa9gAAn2fZg7-IOfY-V4mx3VnhDwoPA_Jzti9E9x-UMcNBzuCs15qRm4CfSiirMe5HaWflrBrBiE4t5QjWR9tJk_ntP5hzjOQCShD8XWj-t1a2oCZzNFIvWP6DF4foppDXwF85FFGDTg-ZE4-dHC3iHY6523KskD9hk4SjHzkn8EVQudnMM5sVQYyp3K9PWAUZ9OQl2n7mDzN8vG5dlbYlCF3w&token_type=Bearer&expires_in=3600&scope=UserApi
    /// 
    /// 到这里获取到token
    /// </summary>
    public class ImplicitInitConfig
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
                    new ApiResource("UserApi", "用户获取API",new List<string>(){IdentityModel.JwtClaimTypes.Role,"eMail" }),//增加cliam
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
                    ClientName="ApiClient for Implicit",
                    ClientSecrets = new [] { new Secret("eleven123456".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Implicit,//隐藏模式
                    RedirectUris={"http://localhost:5726/Ids4/IndexToken" },//可以多个，根据请求来的转发
                    AllowedScopes = new [] { "UserApi","TestApi" },//允许访问的资源
                    AllowAccessTokensViaBrowser=true//允许将token通过浏览器传递
                }
            };
        }
    }
}


