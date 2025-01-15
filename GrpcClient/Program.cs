using Grpc.Net.Client;
using GrpcClient;

GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7219");
var messageClient = new Message.MessageClient(channel);

#region Unary Method
//MessageResponse responseUnary = await messageClient.UnaryCallAsync(new()
//{
//    Name = "Client",
//    Message = "Unary Method.."
//});
//Console.WriteLine(responseUnary.Message);
#endregion

#region Server Streaming Method
//var response = messageClient.StreamingFromServer(new()
//{
//    Name = "Client",
//    Message = "Server Streaming Method.."
//});

//CancellationTokenSource cts = new CancellationTokenSource();

//while (await response.ResponseStream.MoveNext(cts.Token))
//{
//    Console.WriteLine(response.ResponseStream.Current.Message);
//}
#endregion

#region Client Streaming Method
var request = messageClient.StreamingFromClient();

for (int i = 0; i < 10; i++)
{
    await Task.Delay(200);
    await request.RequestStream.WriteAsync(new()
    {
        Name = "Client",
        Message = "Client Streaming Method " + i
    });
}
await request.RequestStream.CompleteAsync();

MessageResponse response = await request.ResponseAsync;
Console.WriteLine(response.Message);
#endregion

Console.Read();
