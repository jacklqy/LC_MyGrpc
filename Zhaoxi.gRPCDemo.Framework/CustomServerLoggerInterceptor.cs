using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.gRPCDemo.Framework
{
    public class CustomServerLoggerInterceptor : Interceptor
    {
        //private readonly ILogger<CustomServerLoggerInterceptor> _logger;

        //public CustomServerLoggerInterceptor(ILogger<CustomServerLoggerInterceptor> logger)
        //{
        //    _logger = logger;
        //}

        /// <summary>
        /// 简单RPC--异步式调用
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            LogAOP<TRequest, TResponse>(MethodType.Unary, context);
            return continuation(request, context);
        }

        private void LogAOP<TRequest, TResponse>(MethodType methodType, ServerCallContext context)
             where TRequest : class
             where TResponse : class
        {
            Console.WriteLine("****************AOP 开始*****************");
            Console.WriteLine($"{context.RequestHeaders[0]}---{context.Host}--{context.Method}--{context.Peer}");
            Console.WriteLine($"Type: {methodType}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}");
            Console.WriteLine("****************AOP 结束*****************");
        }
    }
}
