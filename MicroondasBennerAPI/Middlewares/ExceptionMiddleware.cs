using MicroondasBennerAPI.Utils;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;

namespace MicroondasBennerAPI.Middlewares
{
    public class ExceptionMiddleware()
    {
        public async Task Invoke(HttpContext context)
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature == null || contextFeature.Error == null)
                return;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetErrorCode(contextFeature.Error);
            
            if (contextFeature.Error is UnauthorizedAccessException || contextFeature.Error is ApplicationException)
            {
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { erro = contextFeature.Error.Message }));
                return;
            }

            await context.Response.WriteAsync(
                EnvironmentUtils.IsDevelopment()
                    ? JsonSerializer.Serialize(
                    new ProblemDetails()
                    {
                        Status = context.Response.StatusCode,
                        Title = DateTime.Now.ToString() + " - " + contextFeature.Error.Message,
                        Detail = contextFeature.Error.StackTrace
                    })
                    : "Erro Desconhecido. Comunique o suporte");
        }

        private static HttpStatusCode GetErrorCode(Exception e) 
            => e switch
            {
                ValidationException _ => HttpStatusCode.BadRequest,
                FormatException _ => HttpStatusCode.BadRequest,
                AuthenticationException _ => HttpStatusCode.Forbidden,
                NotImplementedException _ => HttpStatusCode.NotImplemented,
                ApplicationException _ => HttpStatusCode.BadRequest,
                UnauthorizedAccessException _ => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError,
            };
    }
}