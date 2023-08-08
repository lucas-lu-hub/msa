using CommonLib;
using CommonLib.Helper;
using LucasNotes.NoteService.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
var config = builder.Configuration;
builder.Services.AddGrpc();
builder.Services.AddSingleton(new DbHelper(config));
builder.Services.BatchRegisterServices(config);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseGrpcConsul(config);
app.Run();
