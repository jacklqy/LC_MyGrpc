# LC_MyGrpc
Google  RPC

A high-performance, open source universal RPC framework

https://www.grpc.io/

gRPC 一开始由 google 开发，是一款语言中立、平台中立、开源的远程过程调用(RPC)系统。

一个高性能，开源的，跨语言的RPC框架

基于 Http/2 传输协议(支持流) Protocol buffers高效序列化(JSON / XML)

1、grpc框架搭建和调用演示 2、客户端和服务端调用通过AOP进行拦截日志处理 3、通过JWT HS256对称加密进行鉴权授权 4、通过nginx实现grpc负载均衡，以及nginx搭建负载均衡证书问题和忽略证书验证

多方式对比：

1、WebService:最早-门槛最低，soap+xml累赘，只Http，依赖IIS

2、.NetRemoting：RPC--.NET RPC(限制多)---性能高

3、WCF—集大成者，各种服务各种协议—XML 重---.NET5移除WCF(未来可能又有了)

4、Webapi &core webapi 此前，都是当成服务方法(做一件什么事儿)---不满足开放性扩展性---开放的应该是数据而不是业务---建立服务的视角变化了---需要一个新的风格(规范)---
RESTful: 就是一个接口的设计规范，根本出发点就是以资源为核心，支持增删改查(Http-HttpMethod)—uri规范---大家都遵守

5、gRPC Http/2:高性能—专属序列化格式---多路复用双向流---gRPC对js不是很友好，性能也不总是比webapi好(gRPC数据量越大，相对性能越好)。 内部交互，用gRPC；外部数据，用core webapi。


gRPC支持4种流：

1、简单 RPC（Unary RPC）

2、服务端流式 RPC （Server streaming RPC）

3、客户端流式 RPC （Client streaming RPC）

4、双向流式 RPC（Bi-directional streaming RPC）

![image](https://user-images.githubusercontent.com/26539681/120272419-a82dc700-c2df-11eb-93e4-92f321b1c953.png)
![image](https://user-images.githubusercontent.com/26539681/120272624-ff339c00-c2df-11eb-91e2-0ce8d07cc47a.png)
![image](https://user-images.githubusercontent.com/26539681/120272982-76693000-c2e0-11eb-9e2e-4eaf36229000.png)
![image](https://user-images.githubusercontent.com/26539681/120270858-cf36c980-c2dc-11eb-808f-6f25d49a316b.png)
![image](https://user-images.githubusercontent.com/26539681/120271030-1de46380-c2dd-11eb-8507-6d60c70422da.png)

