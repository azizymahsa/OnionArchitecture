using System.Diagnostics;
using System.Net;
using System.Text;
using Domain.DataModel;
using Shared.Model._Base;
using Serilog;
using Shared.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        #region Constructor

        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware
        (
            RequestDelegate next
        )
        {
            _next = next;
        }

        #endregion

        #region Invoke

        public async Task InvokeAsync(HttpContext context)
        {
            var stopWatch = new Stopwatch();
            try
            {
                stopWatch.Start();
                await _next(context);
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                await HandleExceptionAsync(context, ex, stopWatch);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, Stopwatch stopWatch)
        {
            var log = new HttpLog
            {
                IP = context.Connection.RemoteIpAddress?.ToString(),
                URL = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}",
                Method = context.Request.Method,
                RequestHeader = (from t in context.Request.Headers select t).ToDictionary(x => x.Key, x => x.Value.ToArray()),
                RequestedAt = DateTime.Now,
                Request = (await FormatRequest(context.Request)).Replace("\n", string.Empty).Trim(),
                Response = CreateExceptionMessage(ex),
                ResponsedAt = DateTime.Now,
                Duration = stopWatch.ElapsedMilliseconds
            };

            Log.Error(log.ObjectToJson() + "\n");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var responseText = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Detail = "Internal Server Error"
            }.ObjectToJson();

            await context.Response.WriteAsync(responseText);
        }

        #endregion

        #region Request Response

        protected virtual async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            request.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }

        protected virtual async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return text;
        }

        #endregion

        #region Exception

        private string CreateExceptionMessage(Exception ex)
        {
            return $"Message: {ex.Message} | Source: {ex.Source} | StackTrace: {ex.StackTrace}";
        }

        #endregion
    }

    public static class SerilogLogger
    {
        public static void Create()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration)
                .CreateLogger();
        }
    }
}