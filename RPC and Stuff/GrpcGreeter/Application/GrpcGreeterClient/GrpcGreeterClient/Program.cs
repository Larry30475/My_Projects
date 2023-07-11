using System.Text;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeterClient;
using GrpcGreeterClient.Information;

Console.InputEncoding = Encoding.Unicode;
Console.OutputEncoding = Encoding.UTF8;

MyData.info();
Console.WriteLine();

using var channel = GrpcChannel.ForAddress("http://localhost:5075");
var client = new Greeter.GreeterClient(channel);
string fileName = "..\\..\\..\\SendToServerImages\\Kot.jpg";
int bufSize = 4096;

// Reading image
Console.WriteLine("Wybierz opcję:\n1. Przesłać na serwer\n2. Otrzymać od serwera\n3. Wyjść");
Console.Write("Twój wybór: ");
int choice = Convert.ToInt32(Console.ReadLine());
if (choice == 1)
{
    using var fileStream = File.OpenRead(fileName);
    var serverStream = client.SendImageToServer();

    byte[] buffer = new byte[bufSize];
    int counter = 0;
    int bytesRead;
    while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
    {
        await serverStream.RequestStream.WriteAsync(new ImageBytes { Data = ByteString.CopyFrom(buffer, 0, bytesRead)});
        
        Console.WriteLine(String.Format("Numer przesłanej paczki: {0}", ++counter));
        Console.WriteLine(String.Format("Liczba przesłanych bitów w danej paczce: {0}\n", bytesRead));
    }

    await serverStream.RequestStream.CompleteAsync();
    var response = await serverStream.ResponseAsync;
    Console.WriteLine(response.Message);
}
else if (choice == 2)
{
    using var call = client.SendImageToClient(new Google.Protobuf.WellKnownTypes.Empty());
    using var fileStream = File.Create("..\\..\\..\\ReceiveImage\\ImageSent_" + Guid.NewGuid().ToString() + ".jpg");

    int count = 0;
    while (await call.ResponseStream.MoveNext())
    {
        var imageBytes = call.ResponseStream.Current;
        await fileStream.WriteAsync(imageBytes.Data.ToByteArray());

        Console.WriteLine(String.Format("Numer przesłanej paczki: {0}", ++count));
    }

    Console.WriteLine("Plik został poprawnie pobrany");
}
else if (choice == 3)
{
    Console.WriteLine("Żegnam");
}
else
{
    Console.WriteLine("Invalid option");
}
