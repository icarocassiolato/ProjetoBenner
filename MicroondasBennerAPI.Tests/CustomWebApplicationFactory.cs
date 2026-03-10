using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MicroondasBennerAPI.Repository.Contracts;

namespace MicroondasBennerAPI.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("JWT_KEY", "test_jwt_key_1234567890123456");
        Environment.SetEnvironmentVariable("CRYPTO_KEY", "test_crypto_key_123456");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IErroRepository));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddScoped<IErroRepository, NoOpErroRepository>();
        });
    }
}