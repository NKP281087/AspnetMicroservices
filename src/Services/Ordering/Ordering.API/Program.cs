 
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ordering.API.EventBusConsumer;
using Ordering.Application;
using Ordering.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMassTransitHostedService();

IConfiguration configuration = builder.Configuration;
 

InfrastructureServiceRegistration.AddInfrastructureServices(builder.Services, configuration);

builder.Services.AddMassTransit(config => {

    

    //config.UsingRabbitMq((ctx, cfg) => {
    //    cfg.Host(Configuration["EventBusSettings:HostAddress"]);

    //    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
    //    {
    //        c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
    //    });
    //});
});


// General Configuration
//builder.Services.AddScoped<BasketCheckoutConsumer>();
//builder.Services.AddAutoMapper(typeof(StartUp));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
