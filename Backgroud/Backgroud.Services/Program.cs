using Backgroud.Services;
using Backgroud.Services.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<EmailSenderService>();

var host = builder.Build();
host.Run();