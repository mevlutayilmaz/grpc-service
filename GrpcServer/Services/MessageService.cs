﻿using Grpc.Core;

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

        public override async Task StreamingFromServer(MessageRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Message: {request.Message} | Name: {request.Name}");

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new() { Message = "Response Message " + i });
            }
        }

        public override async Task<MessageResponse> StreamingFromClient(IAsyncStreamReader<MessageRequest> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext(context.CancellationToken))
            {
                Console.WriteLine($"Message: {requestStream.Current.Message} | Name: {requestStream.Current.Name}");
            }

            return new() { Message = "Message received successfully.." };
        }

        public override async Task StreamingBothWays(IAsyncStreamReader<MessageRequest> requestStream, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            Task task = Task.Run(async () =>
            {
                while (await requestStream.MoveNext(context.CancellationToken))
                {
                    Console.WriteLine($"Message: {requestStream.Current.Message} | Name: {requestStream.Current.Name}");
                }
            });

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new() { Message = "Response Message " + i });
            }

            await task;
        }
    }
}
