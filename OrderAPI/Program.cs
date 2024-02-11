using System.Reflection;
using eCommerceChoreography.Configuration;
using eCommerceChoreography.DataStructures;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Consumers;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddSwaggerGen()
    .AddMassTransitCfg(builder.Configuration)
    .AddDbConnectionDfg()
    .AddMediatRCfg()
    .AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("v1/swagger.json", "OrderAPI"); });

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();