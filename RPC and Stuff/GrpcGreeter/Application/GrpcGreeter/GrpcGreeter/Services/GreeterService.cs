using Grpc.Core;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace GrpcGreeter.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly string fileName = "SendImage\\8460.jpg";
        private readonly int bufSize = 1024;


        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task<ConfirmationMessage> SendImageToServer(IAsyncStreamReader<ImageBytes> requestStream, ServerCallContext context)
        {
            int count = 0;
            using var fileStream = File.Create("ReceiveImages\\ImageSent_" + Guid.NewGuid().ToString() + ".jpg");
            while (await requestStream.MoveNext())
            {
                var imageBytes = requestStream.Current;
                await fileStream.WriteAsync(imageBytes.Data.ToByteArray());

                Console.WriteLine(String.Format("Numer przesłanej paczki: {0}", ++count));
            }

            return new ConfirmationMessage { Message = "Zdjęcie zostało poprawnie przesłane" };
        }

        public override async Task SendImageToClient(Empty request, IServerStreamWriter<ImageBytes> responseStream, ServerCallContext context)
        {
            using var fileStream = File.OpenRead(fileName);

            byte[] buffer = new byte[bufSize];
            int counter = 0;
            int bytesRead;
            while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
            {
                await responseStream.WriteAsync(new ImageBytes { Data = ByteString.CopyFrom(buffer, 0, bytesRead)});

                Console.WriteLine(String.Format("Numer przesłanej paczki: {0}", ++counter));
                Console.WriteLine(String.Format("Liczba przesłanych bitów w danej paczce: {0}\n", bytesRead));
            }
        }
    }
}