using MicroondasBennerAPI.Helpers;
using MicroondasBennerAPI.Hubs;
using Microsoft.AspNetCore.SignalR;
using Timer = System.Timers.Timer;

namespace MicroondasBennerAPI.Service.Services;

public class MicroondasBackgroundService : BackgroundService, IDisposable
{
    private readonly IHubContext<MicroondasHub> _hubContext;
    private readonly MicroondasService _microondasService;
    private readonly Timer _timer = new(TimeSpan.FromSeconds(1));//Fica mais legível do que new(1000)

    public MicroondasBackgroundService(
        IHubContext<MicroondasHub> hubContext,
        MicroondasService microondasService)
    {
        _hubContext = hubContext;
        _microondasService = microondasService;

        _timer.Elapsed += async (s, e) =>
        {
            _microondasService.Timer();
            var status = _microondasService.GetStatusAtual();
            // Envia para todos os clientes conectados (ou use Groups/Users se precisar por sessão)
            await _hubContext.Clients.All.SendAsync(MetodoHelper.AtualizarProgresso, status);
        };
        _timer.AutoReset = true;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer.Start();
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _timer.Stop();
        _timer.Dispose();
        base.Dispose();
    }
}