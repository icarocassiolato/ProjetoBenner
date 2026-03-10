using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;

namespace MicroondasBennerAPI.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
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
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = problem.Title }));

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