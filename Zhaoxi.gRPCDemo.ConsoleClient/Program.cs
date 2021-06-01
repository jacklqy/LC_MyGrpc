using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using Zhaoxi.gRPCDemo.DefaultServer;

namespace Zhaoxi.gRPCDemo.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Console.WriteLine("Hello World!");
                {
                    //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    //TestHello().Wait();
                }

                {
                    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                    TestMath().Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

        private static async Task TestHello()
        {
            using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync(new HelloRequest { Name = "Eleven" });
                Console.WriteLine("Greeter 服务返回数据: " + reply.Message);

                //client.SayHello(new HelloRequest { Name = "Eleven" });
            }
        }
        private static async Task TestMath()
        {
            #region token
            //string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiRWxldmVuIiwiRU1haWwiOiI1NzI2NTE3N0BxcS5jb20iLCJBY2NvdW50IjoieHV5YW5nQHpoYW94aUVkdS5OZXQiLCJBZ2UiOiIzMyIsIklkIjoiMTIzIiwiTW9iaWxlIjoiMTg2NjQ4NzY2NzEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsIlNleCI6IjEiLCJuYmYiOjE1OTA1OTQ1OTEsImV4cCI6MTU5MDU5ODEzMSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1NzI2IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1NzI2In0.MGXKLQ9ZVh0xvsQ1kNhb5gXi_8hqD2RL8metxhjEFiU";
            //var headers = new Metadata { { "Authorization", $"Bearer {token}" } };
            #endregion

            using (var channel = GrpcChannel.ForAddress("https://localhost:5001"))
            {
                var client = new CustomMath.CustomMathClient(channel);
                //var invoker = channel.Intercept(new CustomClientInterceptor());

                //Console.WriteLine("***************单次调用************");
                //{
                //    var reply = await client.SayHelloAsync(new HelloRequestMath { Name = "Eleven" });
                //    Console.WriteLine($"CustomMath {Thread.CurrentThread.ManagedThreadId} 服务返回数据:{reply.Message} ");
                //}
                //Console.WriteLine("***********************单次调用异步********************************");
                //{
                //    RequestPara requestPara = new RequestPara() { ILeft = 123, IRight = 234 };

                //    var replyPlus = await client.PlusAsync(requestPara);
                //    Console.WriteLine($"CustomMath {Thread.CurrentThread.ManagedThreadId}  服务返回数据:{replyPlus.Result}  Massage={replyPlus.Message}");
                //}
                //Console.WriteLine("***********************单次调用同步********************************");
                //{
                //    var replyPlusSync = client.Plus(new RequestPara() { ILeft = 123, IRight = 234 });
                //    Console.WriteLine($"CustomMath {Thread.CurrentThread.ManagedThreadId}  服务返回数据:{replyPlusSync.Result}  Massage={replyPlusSync.Message}");
                //}
                //Console.WriteLine("***********************单次调用异步带Token********************************");
                //{
                //    RequestPara requestPara = new RequestPara() { ILeft = 123, IRight = 234 };

                //    var replyPlus = await client.PlusAsync(requestPara, headers);
                //    Console.WriteLine($"CustomMath {Thread.CurrentThread.ManagedThreadId}  服务返回数据:{replyPlus.Result}  Massage={replyPlus.Message}");
                //}

                //Console.WriteLine("***********************单次调用同步带Token********************************");
                //{
                //    RequestPara requestPara = new RequestPara() { ILeft = 123, IRight = 234 };
                //    var replyPlusSync = client.Plus(requestPara, headers);
                //    Console.WriteLine($"CustomMath {Thread.CurrentThread.ManagedThreadId}  服务返回数据:{replyPlusSync.Result}  Massage={replyPlusSync.Message}");
                //}

                //Console.WriteLine("**************************空参数*****************************");
                //{
                //    var countResult = await client.CountAsync(new Empty());
                //    Console.WriteLine($"随机一下 {countResult.Count}");
                //    var rand = new Random(DateTime.Now.Millisecond);
                //}
                //Console.WriteLine("**************************客户端流*****************************");
                //{
                //    var bathCat = client.SelfIncreaseClient();
                //    for (int i = 0; i < 10; i++)
                //    {
                //        await bathCat.RequestStream.WriteAsync(new BathTheCatReq() { Id = new Random().Next(0, 20) });
                //        await Task.Delay(100);
                //        Console.WriteLine($"This is {i} Request {Thread.CurrentThread.ManagedThreadId}");
                //    }
                //    Console.WriteLine("**********************************");
                //    //发送完毕
                //    await bathCat.RequestStream.CompleteAsync();
                //    Console.WriteLine("客户端已发送完10个id");
                //    Console.WriteLine("接收结果：");

                //    foreach (var item in bathCat.ResponseAsync.Result.Number)
                //    {
                //        Console.WriteLine($"This is {item} Result");
                //    }
                //    Console.WriteLine("**********************************");
                //}
                //Console.WriteLine("**************************服务端流*****************************");
                //{
                //    IntArrayModel intArrayModel = new IntArrayModel();
                //    for (int i = 0; i < 15; i++)
                //    {
                //        intArrayModel.Number.Add(i);//Number不能直接赋值，
                //    }

                //    CancellationTokenSource cts = new CancellationTokenSource();
                //    cts.CancelAfter(TimeSpan.FromSeconds(2.5)); //指定在2.5s后进行取消操作
                //    var bathCat = client.SelfIncreaseServer(intArrayModel, cancellationToken: cts.Token);

                //    //var bathCat = client.SelfIncreaseServer(intArrayModel);//不带取消
                //    var bathCatRespTask = Task.Run(async () =>
                //    {
                //        await foreach (var resp in bathCat.ResponseStream.ReadAllAsync())
                //        {
                //            Console.WriteLine(resp.Message);
                //            Console.WriteLine($"This is  Response {Thread.CurrentThread.ManagedThreadId}");
                //            Console.WriteLine("**********************************");
                //        }
                //    });
                //    Console.WriteLine("客户端已发送完10个id");
                //    //开始接收响应
                //    await bathCatRespTask;
                //}
                Console.WriteLine("**************************双流*****************************");
                {
                    var bathCat = client.SelfIncreaseDouble();
                    var bathCatRespTask = Task.Run(async () =>
                    {
                        await foreach (var resp in bathCat.ResponseStream.ReadAllAsync())
                        {
                            Console.WriteLine(resp.Message);
                            Console.WriteLine($"This is  Response {Thread.CurrentThread.ManagedThreadId}");
                            Console.WriteLine("**********************************");
                        }
                    });
                    for (int i = 0; i < 10; i++)
                    {
                        await bathCat.RequestStream.WriteAsync(new BathTheCatReq() { Id = new Random().Next(0, 20) });
                        await Task.Delay(100);
                        Console.WriteLine($"This is {i} Request {Thread.CurrentThread.ManagedThreadId}");
                        Console.WriteLine("**********************************");
                    }
                    //发送完毕
                    await bathCat.RequestStream.CompleteAsync();
                    Console.WriteLine("客户端已发送完10个id");
                    Console.WriteLine("接收结果：");
                    //开始接收响应
                    await bathCatRespTask;
                }

                //Console.WriteLine("**************************双流+取消*****************************");
                //{
                //    CancellationTokenSource cts = new CancellationTokenSource();
                //    cts.CancelAfter(TimeSpan.FromSeconds(2.5)); //指定在2.5s后进行取消操作
                //    var bathCat = client.SelfIncreaseDouble(cancellationToken: cts.Token);
                //    var bathCatRespTask = Task.Run(async () =>
                //    {
                //        await foreach (var resp in bathCat.ResponseStream.ReadAllAsync())
                //        {
                //            Console.WriteLine(resp.Message);
                //            Console.WriteLine($"This is  Response {Thread.CurrentThread.ManagedThreadId}");
                //            Console.WriteLine("**********************************");
                //        }
                //    });
                //    for (int i = 0; i < 10; i++)
                //    {
                //        await bathCat.RequestStream.WriteAsync(new BathTheCatReq() { Id = new Random().Next(0, 20) });
                //        await Task.Delay(100);
                //        Console.WriteLine($"This is {i} Request {Thread.CurrentThread.ManagedThreadId}");
                //        Console.WriteLine("**********************************");
                //    }
                //    //发送完毕
                //    await bathCat.RequestStream.CompleteAsync();
                //    Console.WriteLine("客户端已发送完10个id");
                //    Console.WriteLine("接收结果：");
                //    //开始接收响应
                //    await bathCatRespTask;
                //}
            }
        }
    }
}
