using Grpc.Core;

namespace GrpcServer.Services
{
    public class MessageService : Message.MessageBase
    {
        private readonly ILogger<MessageService> _logger;
        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        public override Task<MessageResponse> UnaryCall(MessageRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Message: {request.Message} | Name: {request.Name}");
            MessageResponse response = new() { Message = "Message received successfully.." };
            return Task.FromResult(response);
        }
    }
}
