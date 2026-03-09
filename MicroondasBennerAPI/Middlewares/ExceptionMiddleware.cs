using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroondasBennerAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            var erroRepository = context.RequestServices.GetRequiredService<IErroRepository>();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetErrorCode(exception);

            var problem = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = DateTime.Now + " - " + exception.Message,
                Detail = EnvironmentUtils.IsDevelopment() ? exception.StackTrace : null
            };

            var erroSerializado = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(erroSerializado);

            await erroRepository.InsertAsync(erroSerializado);
        }

        private static HttpStatusCode GetErrorCode(Exception e)
            => e switch
            {
                ValidationException => HttpStatusCode.BadRequest,
                FormatException => HttpStatusCode.BadRequest,
                AuthenticationException => HttpStatusCode.Forbidden,
                NotImplementedException => HttpStatusCode.NotImplemented,
                ApplicationException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };
    }
}