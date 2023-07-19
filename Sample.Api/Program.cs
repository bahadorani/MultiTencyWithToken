using Domino.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Sample.Api;
using Sample.Api.Middleware;
using Sample.Application.Interface;
using Sample.Application.Services;
using Sample.Application.Services.Handler;
using Sample.Domain.Models;
using Sample.Persistence.Context;
using System;

var builder = WebApplication.CreateBuilder(args);

// tenant setter & getter
builder.Services.AddScopedAs<TenantService>(new[] { typeof(ITenantGetter), typeof(ITenantSetter) });

// IOptions version of tenants
builder.Services.Configure<TenantConfigurationSection>(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TenantPolicy", policy =>
        policy.RequireClaim("Tenancy"));
});

builder.Services.AddSingleton<IDbContextFactory, DbContextFactory>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IAccountService,AccountService>();   
builder.Services.AddScoped<ITokenServiceHandle,TokenServiceHandler>();
builder.Services.AddScoped<ITenantService,TenantService>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

app.UseMiddleware<AuthenticationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
