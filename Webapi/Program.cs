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
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

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