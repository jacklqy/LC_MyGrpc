using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Zhaoxi.AspNetCore31.AuthDemo.Utility;
using Zhaoxi.AspNetCore31.AuthDemo.Utility.Auth;
using Zhaoxi.gRPCDemo.DefaultServer;
using Zhaoxi.gRPCDemo.Framework;
using Zhaoxi.gRPCDemo.LessonServer;
using Zhaoxi.gRPCDemo.UserServer;

namespace Zhaoxi.AspNetCore31.AuthDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()//�����ռ�dll-������--action--PartManager
                .AddNewtonsoftJson();
            //services.AddAuthorization()
            //services.AddAuthorizationCore();
            //services.AddAuthorizationPolicyEvaluator();

            #region Filter��ʽ
            //services.AddAuthentication()
            //.AddCookie();
            #endregion

            #region Authorize
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //.AddCookie();
            #endregion

            #region �������֤--�Զ���Handler
            //services.AddAuthenticationCore();
            //services.AddAuthentication().AddCookie();
            //services.AddAuthenticationCore(options => options.AddScheme<CustomHandler>("CustomScheme", "DemoScheme"));
            #endregion

            #region ����Cookie��Ȩ
            //services.AddScoped<ITicketStore, MemoryCacheTicketStore>();
            //services.AddMemoryCache();
            //////services.AddDistributedRedisCache(options =>
            //////{
            //////    options.Configuration = "127.0.0.1:6379";
            //////    options.InstanceName = "RedisDistributedSession";
            //////});
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;//������
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = "Cookie/Login";
            //})
            //.AddCookie(options =>
            //{
            //    //��Ϣ���ڷ����--��keyд��cookie--����session
            //    options.SessionStore = services.BuildServiceProvider().GetService<ITicketStore>();
            //    options.Events = new CookieAuthenticationEvents()
            //    {
            //        OnSignedIn = new Func<CookieSignedInContext, Task>(
            //            async context =>
            //            {
            //                Console.WriteLine($"{context.Request.Path} is OnSignedIn");
            //                await Task.CompletedTask;
            //            }),
            //        OnSigningIn = async context =>
            //         {
            //             Console.WriteLine($"{context.Request.Path} is OnSigningIn");
            //             await Task.CompletedTask;
            //         },
            //        OnSigningOut = async context =>
            //        {
            //            Console.WriteLine($"{context.Request.Path} is OnSigningOut");
            //            await Task.CompletedTask;
            //        }
            //    };//��չ�¼�
            //});

            ////new AuthenticationBuilder().AddCookie()
            #endregion

            #region ����Cookies��Ȩ---��ɫ��Ȩ
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    //������,signin signout Authenticate���ǻ���Scheme
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //{
            //    options.LoginPath = "/Authorization/Index";
            //    options.AccessDeniedPath = "/Authorization/Index";
            //});
            ////.AddCookie("CustomScheme", options =>
            ////{
            ////    options.LoginPath = "/Authorization/Index";
            ////    options.AccessDeniedPath = "/Authorization/Index";
            ////});
            #endregion

            #region ���ڲ�����Ȩ
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;//������
            //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //{
            //    options.LoginPath = "/Authorization/Index";
            //    options.AccessDeniedPath = "/Authorization/Index";
            //});

            //////����һ�����õ�policy
            ////var qqEmailPolicy = new AuthorizationPolicyBuilder().AddRequirements(new QQEmailRequirement()).Build();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireRole("Admin")//Claim��Role��Admin
            //        .RequireUserName("Eleven")//Claim��Name��Eleven
            //        .RequireClaim(ClaimTypes.Email)//������ĳ��Cliam
            //        //.Combine(qqEmailPolicy)
            //        );//����

            //    options.AddPolicy("UserPolicy",
            //        policyBuilder => policyBuilder.RequireAssertion(context =>
            //        context.User.HasClaim(c => c.Type == ClaimTypes.Role)
            //        && context.User.Claims.First(c => c.Type.Equals(ClaimTypes.Role)).Value == "Admin")
            //   //.Combine(qqEmailPolicy)
            //   );//�Զ���
            //    //policy����  û��Requirements


            //    //options.AddPolicy("QQEmail", policyBuilder => policyBuilder.Requirements.Add(new QQEmailRequirement()));
            //    options.AddPolicy("DoubleEmail", policyBuilder => policyBuilder.Requirements.Add(new DoubleEmailRequirement()));
            //});
            //services.AddSingleton<IAuthorizationHandler, ZhaoxiMailHandler>();
            //services.AddSingleton<IAuthorizationHandler, QQMailHandler>();
            #endregion

            #region jwtУ��  HS
            //JWTTokenOptions tokenOptions = new JWTTokenOptions();
            //Configuration.Bind("JWTTokenOptions", tokenOptions);

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        //JWT��һЩĬ�ϵ����ԣ����Ǹ���Ȩʱ�Ϳ���ɸѡ��
            //        ValidateIssuer = true,//�Ƿ���֤Issuer
            //        ValidateAudience = true,//�Ƿ���֤Audience
            //        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
            //        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
            //        ValidAudience = tokenOptions.Audience,//
            //        ValidIssuer = tokenOptions.Issuer,//Issuer���������ǰ��ǩ��jwt������һ��
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),//�õ�SecurityKey
            //        //AudienceValidator = (m, n, z) =>
            //        //{
            //        //    //��ͬ��ȥ��չ����Audience��У�����---��Ȩ
            //        //    return m != null && m.FirstOrDefault().Equals(this.Configuration["audience"]);
            //        //},
            //        //LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
            //        //{
            //        //    return notBefore <= DateTime.Now
            //        //    && expires >= DateTime.Now;
            //        //    //&& validationParameters
            //        //}//�Զ���У�����
            //    };
            //});
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireRole("Admin")//Claim��Role��Admin
            //        .RequireUserName("Eleven")//Claim��Name��Eleven
            //        .RequireClaim("EMail")//������ĳ��Cliam
            //         .RequireClaim("Account")
            //        //.Combine(qqEmailPolicy)
            //        .AddRequirements(new CustomExtendRequirement())
            //        );//����

            //    //options.AddPolicy("QQEmail", policyBuilder => policyBuilder.Requirements.Add(new QQEmailRequirement()));
            //    options.AddPolicy("DoubleEmail", policyBuilder => policyBuilder
            //    .AddRequirements(new CustomExtendRequirement())
            //    .Requirements.Add(new DoubleEmailRequirement()));
            //});
            //services.AddSingleton<IAuthorizationHandler, ZhaoxiMailHandler>();
            //services.AddSingleton<IAuthorizationHandler, QQMailHandler>();
            //services.AddSingleton<IAuthorizationHandler, CustomExtendRequirementHandler>();
            #endregion

            #region jwtУ��  RS

            //#region ��ȡ��Կ
            //string path = Path.Combine(Directory.GetCurrentDirectory(), "key.public.json");
            //string key = File.ReadAllText(path);//this.Configuration["SecurityKey"];
            //Console.WriteLine($"KeyPath:{path}");

            //var keyParams = JsonConvert.DeserializeObject<RSAParameters>(key);
            //foreach (var item in keyParams.GetType().GetFields())
            //{
            //    Console.WriteLine($"{item.Name}:{item.GetValue(keyParams)}");
            //}
            ////var credentials = new SigningCredentials(new RsaSecurityKey(keyParams), SecurityAlgorithms.RsaSha256Signature);
            //#endregion

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,//�Ƿ���֤Issuer
            //        ValidateAudience = true,//�Ƿ���֤Audience
            //        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
            //        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
            //        #region MyRegion
            //        //ValidAudience = this.Configuration["Audience"],//Audience
            //        //ValidIssuer = this.Configuration["Issuer"],//Issuer���������ǰ��ǩ��jwt������һ��
            //        #endregion
            //        ValidAudience = this.Configuration["JWTTokenOptions:Audience"],//Audience
            //        ValidIssuer = this.Configuration["JWTTokenOptions:Issuer"],//Issuer���������ǰ��ǩ��jwt������һ��
            //        IssuerSigningKey = new RsaSecurityKey(keyParams),
            //        IssuerSigningKeyValidator = (m, n, z) =>
            //         {
            //             Console.WriteLine("This is IssuerSigningKeyValidator");
            //             return true;
            //         },
            //        //IssuerValidator = (m, n, z) =>
            //        // {
            //        //     Console.WriteLine($"This is IssuerValidator {this.Configuration["JWTTokenOptions:Issuer"]}");
            //        //     return "http://localhost:5726";
            //        // },
            //        AudienceValidator = (m, n, z) =>
            //        {
            //            Console.WriteLine("This is AudienceValidator");
            //            return true;
            //            //return m != null && m.FirstOrDefault().Equals(this.Configuration["Audience"]);
            //        },//�Զ���У����򣬿����µ�¼��֮ǰ����Ч
            //    };
            //});
            #endregion

            #region IdentityServer4--Client
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:7200";//ids4�ĵ�ַ
            //        options.ApiName = "UserApi";
            //        options.RequireHttpsMetadata = false;
            //    });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("eMailPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireAssertion(context =>
            //        context.User.HasClaim(c => c.Type == "client_eMail")
            //        && context.User.Claims.First(c => c.Type.Equals("client_eMail")).Value.EndsWith("@qq.com")));//Client
            //});
            #endregion

            #region IdentityServer4--Password
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:7200";
            //        options.ApiName = "TestApi";
            //        options.RequireHttpsMetadata = false;
            //    });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("eMailPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireAssertion(context =>
            //        context.User.HasClaim(c => c.Type == "eMail")
            //        && context.User.Claims.First(c => c.Type.Equals("eMail")).Value.EndsWith("@qq.com")));//Client
            //});
            #endregion

            #region IdentityServer4--Implicit
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:7200";
            //        options.ApiName = "UserApi";
            //        options.RequireHttpsMetadata = false;
            //    });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("eMailPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireAssertion(context =>
            //        context.User.HasClaim(c => c.Type == "eMail")
            //        && context.User.Claims.First(c => c.Type.Equals("eMail")).Value.EndsWith("@qq.com")));//Client
            //});
            #endregion

            #region IdentityServer4--Code
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:7200";
            //        options.ApiName = "UserApi";
            //        options.RequireHttpsMetadata = false;
            //    });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("eMailPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireAssertion(context =>
            //        context.User.HasClaim(c => c.Type == "eMail")
            //        && context.User.Claims.First(c => c.Type.Equals("eMail")).Value.EndsWith("@qq.com")));//Client
            //});
            #endregion

            #region IdentityServer4--Hybrid
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = "http://localhost:7200";
            //        options.ApiName = "UserApi";
            //        options.RequireHttpsMetadata = false;
            //    });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("eMailPolicy",
            //        policyBuilder => policyBuilder
            //        .RequireAssertion(context =>
            //        context.User.HasClaim(c => c.Type == "eMail")
            //        && context.User.Claims.First(c => c.Type.Equals("eMail")).Value.EndsWith("@qq.com")));//Client
            //});
            #endregion


            #region gRPC
            //���й���
            services.AddGrpcClient<CustomMath.CustomMathClient>(options =>
            {
                options.Address = new Uri("https://localhost:5001");
                options.Interceptors.Add(new CustomClientLoggerInterceptor());
            });

            services.AddGrpcClient<ZhaoxiLesson.ZhaoxiLessonClient>(options =>
            {
                options.Address = new Uri("https://localhost:6001");
                options.Interceptors.Add(new CustomClientLoggerInterceptor());
            }).ConfigureChannel(grpcOptions =>
            {
                var callCredentials = CallCredentials.FromInterceptor(async (context, metadata) =>
                {
                    string token = JWTTokenHelper.GetJWTToken().Result;//��ʱ��ȡ��--������Լ�һ�㻺�棬���ʧЧ����ȥ����
                    Console.WriteLine($"token:{token}");
                    metadata.Add("Authorization", $"Bearer {token}");
                });
                grpcOptions.Credentials = ChannelCredentials.Create(new SslCredentials(), callCredentials);
                //���󶼴���token��Ҳ�����ڵ��÷���ʱ���ݣ� var replyPlus = await client.PlusAsync(requestPara, headers);
            });

            services.AddGrpcClient<ZhaoxiUser.ZhaoxiUserClient>(options =>
            {
                options.Address = new Uri("https://localhost:443");
                options.Interceptors.Add(new CustomClientLoggerInterceptor());
            }).ConfigureChannel(grpcOptions =>
            {
                Console.WriteLine("This ZhaoxiUser.ZhaoxiUserClien ConfigureChannel");
                grpcOptions.HttpClient = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (msg, cert, chain, error) => true//����֤��
                });//HttpClient--443 ���� grpc-https://localhost:5001
            });
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            });

            app.UseRouting();

            #region ����CookieAuthentication
            //app.UseAuthentication();//��Ȩ
            #endregion

            #region  JWT
            //app.UseAuthentication();//��Ȩ��������Ϣ--���Ƕ�ȡtoken������token
            #endregion

            #region  Ids4
            app.UseAuthentication();
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
