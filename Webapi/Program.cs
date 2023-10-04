using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog.Core;
using Webapi.Configuration;
using Webapi.Logs;
using Webapi.Middleware;

var builder = WebApplication.CreateBuilder(args);

Configurationinstall.GetDbContext(builder.Services, builder.Configuration);
// Add services to the container.
Configurationinstall.GetRepository(builder.Services);
Configurationinstall.GetServices(builder.Services);

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors();
Configurationinstall.Authentication(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

//Log 
app.UseMiddleware<RequestLoggingMiddleware>();
// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
// write log file
app.Configuration(builder.Configuration);

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Developed-By", "vancong");
    context.Response.Headers.Add("Server", "docker");
    context.Response.Headers.Add("x-powered-by","docker");
    await next.Invoke();
});

app.Run();