using Grpc.Net.Client;
using GrpcClient;

GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7219");
var messageClient = new Message.MessageClient(channel);

#region Unary Method
//MessageResponse responseUnary = await messageClient.UnaryCallAsync(new()
//{
//    Name = "Mevlüt",
//    Message = "Unary Method.."
//});
//Console.WriteLine(responseUnary.Message);
#endregion

#region Server Streaming Method
//var response = messageClient.StreamingFromServer(new()
//{
//    Name = "Mevlüt",
//    Message = "Server Streaming Method.."
//});

//CancellationTokenSource cts = new CancellationTokenSource();

//while (await response.ResponseStream.MoveNext(cts.Token))
//{
//    Console.WriteLine(response.ResponseStream.Current.Message);
//}
#endregion

Console.Read();
