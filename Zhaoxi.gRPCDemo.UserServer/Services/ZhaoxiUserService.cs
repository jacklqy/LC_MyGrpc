using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.gRPCDemo.UserServer.Services
{
    public class ZhaoxiUserService : ZhaoxiUser.ZhaoxiUserBase
    {
        public override Task<ZhaoxiUserReply> FindUser(ZhaoxiUserRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ZhaoxiUserReply()
            {
                User = new ZhaoxiUserReply.Types.UserModel()
                {
                    Id = request.Id,
                    Name = "GoodLuck" + new Random().Next(1, 1561) % 156,//索引对应
                    Account = "57265177@qq.com",
                    Password = "123456"
                }
            });
        }

    }
}
