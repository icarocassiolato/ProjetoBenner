using MicroondasApi.Hubs;
using MicroondasBenner.Services;
using MicroondasApi.Service.Contracts;
using MicroondasApi.Service.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<MicroondasService>();
builder.Services.AddHostedService<MicroondasBackgroundService>();
builder.Services.AddScoped<IMicroondasAppService, MicroondasAppService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();
app.MapHub<MicroondasHub>("/microondasHub");

app.Run();