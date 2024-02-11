using PaymentAPI.Configuration.cs;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddMassTransitCfg(builder.Configuration);

var app = builder.Build();

app.Run();