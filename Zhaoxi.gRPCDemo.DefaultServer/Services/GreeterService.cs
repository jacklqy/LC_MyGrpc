using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Zhaoxi.gRPCDemo.DefaultServer
{
    /// <summary>
    /// 服务接口
    /// 
    /// 语言无关---模板(接口)---C#代码--接口/基类
    /// GreeterService严格遵循了gRPC的格式要求
    /// 还约束了序列化的工具
    /// 
    /// wsdl类似
    /// </summary>
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
