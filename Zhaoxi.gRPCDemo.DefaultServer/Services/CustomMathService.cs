using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Zhaoxi.gRPCDemo.DefaultServer.Services
{
    public class CustomMathService : CustomMath.CustomMathBase//自动生成
    {
        private readonly ILogger<CustomMathService> _logger;
        public CustomMathService(ILogger<CustomMathService> logger)
        {
            _logger = logger;
        }


        public override Task<HelloReplyMath> SayHello(HelloRequestMath request, ServerCallContext context)
        {
            return Task.FromResult<HelloReplyMath>(new HelloReplyMath()
            {
                Message = $"This is {request.Id}-{request.Name}"
            });
        }

        public override Task<CountResult> Count(Empty request, ServerCallContext context)
        {

            return Task.FromResult(new CountResult()
            {
                Count = DateTime.Now.Year
            });
        }

        public override Task<ResponseResult> Plus(RequestPara request, ServerCallContext context)
        {
            int iResult = request.ILeft + request.IRight;
            ResponseResult responseResult = new ResponseResult()
            {
                Result = iResult,
                Message = "Success"
            };

            return Task.FromResult(responseResult);
        }
        /// <summary>
        /// 不是一次性计算完，全部返回结果，而是分批次返回
        /// 15*500 然后一起返回
        /// 
        /// 500  500  500  500==
        /// 
        /// yield状态机
        /// </summary>
        /// <param name="request"></param>
        /// <param name="responseStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task SelfIncreaseServer(IntArrayModel request, IServerStreamWriter<BathTheCatResp> responseStream, ServerCallContext context)
        {
            foreach (var item in request.Number)
            {
                int number = item;
                Console.WriteLine($"This is {number} invoke");
                await responseStream.WriteAsync(new BathTheCatResp() { Message = $"number++ ={++number}！" });
                await Task.Delay(500);//此处主要是为了方便客户端能看出流调用的效果
            }
            //难道不是一次性返回全部数据，而是一点点的返回？
        }

        public override async Task<IntArrayModel> SelfIncreaseClient(IAsyncStreamReader<BathTheCatReq> requestStream, ServerCallContext context)
        {
            IntArrayModel intArrayModel = new IntArrayModel();
            while (await requestStream.MoveNext())
            {
                intArrayModel.Number.Add(requestStream.Current.Id + 1);
                Console.WriteLine($"SelfIncreaseClient Number {requestStream.Current.Id} 获取到并处理.");
                Thread.Sleep(100);
            }
            return intArrayModel;
        }

        public override async Task SelfIncreaseDouble(IAsyncStreamReader<BathTheCatReq> requestStream, IServerStreamWriter<BathTheCatResp> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                Console.WriteLine($"SelfIncreaseDouble Number {requestStream.Current.Id} 获取到并处理.");
                await responseStream.WriteAsync(new BathTheCatResp() { Message = $"number++ ={requestStream.Current.Id + 1}！" });
                await Task.Delay(500);//此处主要是为了方便客户端能看出流调用的效果
            }

        }
    }
}
