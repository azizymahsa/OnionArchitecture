using DataAccess;
using Domain.Dto._Base;
using ExternalService;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Service;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped(provider => provider.GetRequiredService<IActionContextAccessor>().ActionContext.ModelState);

builder.Services.AddSingleton(builder.Configuration.Get<ApplicationSettingsDto>());

builder.Services.RegisterDataAccess();
builder.Services.RegisterServices();
builder.Services.RegisterExternalServices();

SerilogLogger.Create();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();