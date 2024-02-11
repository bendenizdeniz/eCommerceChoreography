using System.Reflection;
using MediatR;
using StockAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwaggerGen()
    .AddMassTransitCfg(builder.Configuration)
    .AddDbConnCfg()
    .AddMediatR(Assembly.GetExecutingAssembly())
    .AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("v1/swagger.json", "StockAPI"); });

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();