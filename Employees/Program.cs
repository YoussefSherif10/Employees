using System.Net;
using Employees.Data;
using Employees.Extensions;
using Employees.Models.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder
    .Services
    .AddControllers(options =>
    {
        options.RespectBrowserAcceptHeader = true;
        options.ReturnHttpNotAcceptable = true;
    })
    .AddXmlDataContractSerializerFormatters();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder
    .Services
    .AddDbContext<AppDbContext>(
        options => options.UseSqlite(builder.Configuration.GetConnectionString("SqlConnection"))
    );

var app = builder.Build();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            await context
                .Response
                .WriteAsync(
                    new ErrorDetails
                    {
                        StatusCode = contextFeature.Error switch
                        {
                            InvalidOperationException => StatusCodes.Status404NotFound,
                            BadHttpRequestException => StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        },
                        Message = contextFeature.Error.Message
                    }.ToString()
                );
        }
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
