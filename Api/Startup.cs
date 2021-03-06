﻿using System;
using Cashflow.Api.Extensions;
using Cashflow.Api.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Cashflow.Api
{
  public class Startup
  {
    private IHostEnvironment _env;

    public Startup(IHostEnvironment env)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddEnvironmentVariables();

      builder = builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      Configuration = builder.Build();

      _env = env;
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      string connectionString = null;
      string jwtKey = null;
      if (_env.IsDevelopment())
      {
        connectionString = Configuration["ConnectionString"];
        jwtKey = Configuration["JwtKey"];
      }
      else
      {
        connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        jwtKey = Environment.GetEnvironmentVariable("JwtKey");
      }

      services.AddControllers();
      services.ConfigureScopes();
      services.ConfigureAuthentication(jwtKey);
      services.AddRouting();
      services.AddSingleton(typeof(AppConfig), new AppConfig(jwtKey, connectionString));

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cashflow API", Version = "v1" });
        var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        var filePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, $"{appName}.xml");
        c.IncludeXmlComments(filePath);
        c.AddSecurityDefinition("Token", new OpenApiSecurityScheme
        {
          Name = "Bearer token",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey
        });
      });
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseMiddleware(typeof(ExceptionHandler));
      app.UseAuthentication();
      app.UseRouting();
      app.UseAuthorization();
      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cashflow API V1");
      });

      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}