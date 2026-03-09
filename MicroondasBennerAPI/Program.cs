using DotNetEnv;
using MicroondasBennerAPI.Connection.Contracts;
using MicroondasBennerAPI.Connection.Factory;
using MicroondasBennerAPI.Middlewares;
using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Repository.Repositories;
using MicroondasBennerAPI.Service.Contracts;
using MicroondasBennerAPI.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
var key = Encoding.ASCII.GetBytes(jwtKey);

builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>();
builder.Services.AddScoped<IProgramasPersonalizadosRepository, ProgramasPersonalizadosRepository>();
builder.Services.AddScoped<IProgramasPersonalizadosService, ProgramasPersonalizadosService>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services
    .AddHealthChecks()
    .AddCheck<HealthCheckMiddleware>("Sample");

#region Autenticação JWT

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

#endregion

var app = builder.Build();

app.UseExceptionHandler(
    new ExceptionHandlerOptions
    {
        ExceptionHandler = new ExceptionMiddleware().Invoke
    }
);

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/HealthCheck");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.MapControllers();

app.Run();
