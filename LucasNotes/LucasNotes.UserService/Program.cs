using CommonLib;
using Consul;
using LucasNotes.UserService;
using LucasNotes.UserService.Repositories;
using LucasNotes.UserService.Services;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton(new DbHelper(config));
builder.Services.BatchRegisterServices(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<UserManagerService>();
app.MapGrpcService<HealthCheckService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseConsul(config);
app.Run();
