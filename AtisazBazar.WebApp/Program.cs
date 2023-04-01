using AspNetCoreRateLimit;
using AtisazBazar.WebApp.HealthCheck;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpLogging;
using Prometheus;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);
// Add serilog services
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add application services
builder.Services.AddServices(builder.Configuration);
// Add request log
builder.Services.AddHttpLogging(opts =>
{
    opts.LoggingFields = HttpLoggingFields.ResponseBody;
});

// Add Ip Rate Limiting
builder.Services.AddIpRateLimit();

// Add redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["ConnectionStrings:Redis"];
    options.InstanceName = "master";
});

// Add HealthCheck
builder.Services.AddHealthCheck(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add request log
app.UseHttpLogging();
// Add prometheus-net 
app.UseMetricServer();
//  export HTTP metrics to Prometheus
app.UseHttpMetrics();
// Ip Rate Limit
app.UseIpRateLimiting();
// Map HealthChecks
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Add security services
app.UseHsts(options => options.MaxAge(days: 30));
app.UseXContentTypeOptions();
app.UseXXssProtection(options => options.EnabledWithBlockMode());
app.UseXfo(options => options.SameOrigin());
app.UseReferrerPolicy(opts => opts.NoReferrerWhenDowngrade());
// Add HealthChecks UI
app.MapHealthChecksUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
