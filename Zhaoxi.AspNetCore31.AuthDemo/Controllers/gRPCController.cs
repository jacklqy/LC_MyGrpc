using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zhaoxi.gRPCDemo.DefaultServer;
using Zhaoxi.gRPCDemo.LessonServer;
using Zhaoxi.gRPCDemo.UserServer;

namespace Zhaoxi.AspNetCore31.AuthDemo.Controllers
{
    public class gRPCController : Controller
    {
        private readonly ILogger<gRPCController> _logger;
        private readonly CustomMath.CustomMathClient _customMathClient;
        private readonly ZhaoxiLesson.ZhaoxiLessonClient _lessonClient;
        private readonly ZhaoxiUser.ZhaoxiUserClient _userClient;
        public gRPCController(ILogger<gRPCController> logger
            , CustomMath.CustomMathClient customMathClient
            , ZhaoxiLesson.ZhaoxiLessonClient lessonClient
            , ZhaoxiUser.ZhaoxiUserClient userClient)
        {
            _logger = logger;
            this._customMathClient = customMathClient;
            this._lessonClient = lessonClient;
            this._userClient = userClient;
        }

        public async Task<IActionResult> Index()
        {
            //using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
            //{
            //    var client = new CustomMath.CustomMathClient(channel);

            //    Console.WriteLine("***************单次调用************");
            //    {
            //        var reply = await client.SayHelloAsync(new HelloRequestMath { Name = "Eleven" });
            //        Console.WriteLine($"CustomMath {Thread.CurrentThread.ManagedThreadId} 服务返回数据:{reply.Message} ");
            //    }
            //}
            {
                var reply = await this._customMathClient.SayHelloAsync(new HelloRequestMath { Name = "Eleven1" });
                Console.WriteLine($"CustomMath {Thread.CurrentThread.ManagedThreadId} 服务返回数据1:{reply.Message} ");
            }
            {
                var reply = this._customMathClient.SayHello(new HelloRequestMath { Name = "Eleven2" });
                Console.WriteLine($"CustomMath {Thread.CurrentThread.ManagedThreadId} 服务返回数据2:{reply.Message} ");
            }
            {
                //var reply = await this._lessonClient.FindLessonAsync(new ZhaoxiLessonRequest() { Id = 123 });

                //#region MyRegion
                //string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRWxldmVuIiwiRU1haWwiOiI1NzI2NTE3N0BxcS5jb20iLCJBY2NvdW50IjoieHV5YW5nQHpoYW94aUVkdS5OZXQiLCJBZ2UiOiIzMyIsIklkIjoiMTIzIiwiTW9iaWxlIjoiMTg2NjQ4NzY2NzEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsIlNleCI6IjEiLCJuYmYiOjE1OTA3NTgzNDcsImV4cCI6MTU5MDc2MTg4NywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1NzI2IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1NzI2In0.7vMHx62XENyhkksCjnT5AeT78K3zG-z7B3hzv8DGPDI";
                //var headers = new Metadata { { "Authorization", $"Bearer {token}" } };

                //var reply = await this._lessonClient.FindLessonAsync(new ZhaoxiLessonRequest() { Id = 123 },
                //    headers: headers);
                //#endregion

                var reply = await this._lessonClient.FindLessonAsync(new ZhaoxiLessonRequest() { Id = 123 });
                Console.WriteLine($"_lessonClient {Thread.CurrentThread.ManagedThreadId} 服务返回数据3:{Newtonsoft.Json.JsonConvert.SerializeObject(reply.Lesson)} ");
            }

            {
                var reply = await this._userClient.FindUserAsync(new ZhaoxiUserRequest() { Id = 123 });
                Console.WriteLine($"_userClient {Thread.CurrentThread.ManagedThreadId} 服务返回数据4:{Newtonsoft.Json.JsonConvert.SerializeObject(reply.User)} ");
                base.ViewBag.Luck = reply.User.Name;
            }
            return View();
        }
    }
}