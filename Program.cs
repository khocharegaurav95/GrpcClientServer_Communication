using Grpc.Net.Client;
using System;
using System.Net;

namespace GrpcClient
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            using var channel= GrpcChannel.ForAddress("https://localhost:5001");

            var input = new HelloRequest { Name = "Greeter Client" };
            var client= new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(input);
            
            Console.WriteLine($"Greetings:{reply.Message}");
            

            Console.WriteLine("Press Any Key to Exit...");
            Console.ReadLine();
        }
    }
}

