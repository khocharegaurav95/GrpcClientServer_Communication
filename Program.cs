using GrpcServer.Services;
using Grpc.Net.Client;
using System;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
namespace GrpcClient
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            using var channel= GrpcChannel.ForAddress("https://localhost:5001");

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
            var input = new HelloRequest { Name = "Greeter Client" };
            var client= new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(input);

// Add services to the container.
builder.Services.AddGrpc();
            Console.WriteLine($"Greetings:{reply.Message}");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            Console.WriteLine("Press Any Key to Exit...");
            Console.ReadLine();
        }
    }
}

app.Run();
