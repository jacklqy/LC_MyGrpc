using Grpc.Core;
using Grpc.Core.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zhaoxi.gRPCDemo.Framework
{
    public class CustomClientLoggerInterceptor : Interceptor
    {
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
           TRequest request,
           ClientInterceptorContext<TRequest, TResponse> context,
           AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            this.LogAOP(context.Method);
            return continuation(request, context);
        }

        private void LogAOP<TRequest, TResponse>(Method<TRequest, TResponse> method)
            where TRequest : class
            where TResponse : class
        {
            Console.WriteLine("****************AOP 开始*****************");
            Console.WriteLine($"{method.Name}---{method.FullName}--{method.ServiceName}");
            Console.WriteLine($"Type: {method.Type}. Request: {typeof(TRequest)}. Response: {typeof(TResponse)}");
            Console.WriteLine("****************AOP 结束*****************");
        }

    }
}
