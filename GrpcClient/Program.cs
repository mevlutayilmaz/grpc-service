using Grpc.Net.Client;
using GrpcClient;

GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7219");
var messageClient = new Message.MessageClient(channel);

#region Unary Method
MessageResponse response = await messageClient.UnaryCallAsync(new()
{
    Name = "Mevlüt",
    Message = "Hello World!"
});
Console.WriteLine(response.Message);
#endregion


Console.Read();
