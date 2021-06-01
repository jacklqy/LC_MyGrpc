// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/ZhaoxiLesson.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Zhaoxi.gRPCDemo.LessonServer {
  /// <summary>
  /// The greeting service definition.
  /// </summary>
  public static partial class ZhaoxiLesson
  {
    static readonly string __ServiceName = "ZhaoxiLesson.ZhaoxiLesson";

    static readonly grpc::Marshaller<global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonRequest> __Marshaller_ZhaoxiLesson_ZhaoxiLessonRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonReply> __Marshaller_ZhaoxiLesson_ZhaoxiLessonReply = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonReply.Parser.ParseFrom);

    static readonly grpc::Method<global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonRequest, global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonReply> __Method_FindLesson = new grpc::Method<global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonRequest, global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "FindLesson",
        __Marshaller_ZhaoxiLesson_ZhaoxiLessonRequest,
        __Marshaller_ZhaoxiLesson_ZhaoxiLessonReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ZhaoxiLesson</summary>
    [grpc::BindServiceMethod(typeof(ZhaoxiLesson), "BindService")]
    public abstract partial class ZhaoxiLessonBase
    {
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonReply> FindLesson(global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ZhaoxiLessonBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_FindLesson, serviceImpl.FindLesson).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ZhaoxiLessonBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_FindLesson, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonRequest, global::Zhaoxi.gRPCDemo.LessonServer.ZhaoxiLessonReply>(serviceImpl.FindLesson));
    }

  }
}
#endregion
