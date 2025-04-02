using Grpc.Core;
using SP25_NET1718_PRN231_ASM2_SE173443_KhanhTD;

namespace SP25_NET1718_PRN231_ASM2_SE173443_KhanhTD.Services
{
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
