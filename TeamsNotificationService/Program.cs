using TeamsNotificationService.Framework;
using TeamsNotificationService.Framework.Implementations;
using TeamsNotificationService.Services;
using TeamsNotificationService.System;
using TeamsNotificationService.System.Implementations;
using TeamsNotificationService.Services.Implementations;
using Polly;
using Polly.Contrib.WaitAndRetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IIOWrapper, IOWrapper>();
builder.Services.AddScoped<IJsonWrapper, JsonWrapper>();
builder.Services.AddHttpClient<IHttpWrapper, HttpWrapper>()
    .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder
    .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));
builder.Services.AddScoped<IConfigurationService, NotificationConfigurationProvider>();
builder.Services.AddScoped<INotificationSender, TeamsNotificationSender>();
builder.Services.AddScoped<INotificator, Notificator>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
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