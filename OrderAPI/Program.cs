using System.Reflection;
using eCommerceChoreography.DataStructures;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddMassTransit(configurator =>
{
    configurator.UsingRabbitMq((context, busConfigurator) =>
        {
            busConfigurator.Host(
                "amqps://fskgqixc:r_C4CG8NFB78dEF175g_PRA7Drcbguj3@moose.rmq.cloudamqp.com/fskgqixc"); //ENV'YE EKLENECEK.
        }
    );
});

builder.Services.AddDbContext<OrderAPIDbContext>(options =>
    options.UseSqlServer("Server=localhost,1433;Database=ChoreographyDB;User Id=sa;Password=Password1;Encrypt=False;"));


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt => { opt.SwaggerEndpoint("v1/swagger.json", "OrderAPI"); });

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();