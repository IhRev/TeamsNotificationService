﻿using TeamsNotificationService.Framework;
using TeamsNotificationService.Framework.Implementations;
using TeamsNotificationService.Services;
using TeamsNotificationService.System;
using TeamsNotificationService.System.Implementations;
using TeamsNotificationService.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IIOWrapper, IOWrapper>();
builder.Services.AddScoped<IJsonWrapper, JsonWrapper>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<INotificator, Notificator>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();