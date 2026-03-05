using MicroondasBenner.Helpers;
using MicroondasBenner.Services;
using Microsoft.AspNetCore.SignalR;

namespace MicroondasBenner.Hubs;

//Não precisaria dessa classe se o timer fosse implementado dentro do MicroondasService, mas assim fica mais organizado e desacoplado
//Usei SignalR para comunicação em tempo real, mas poderia ser feito com polling ou Server-Sent Events
public class MicroondasHub(MicroondasService microondasService) : Hub
{
    private readonly MicroondasService _microondasService = microondasService;

    public async Task Iniciar(int tempo, int potencia)
    {
        if (!_microondasService.IsValido(tempo, potencia, out var erro))
        {
            await Clients.Caller.SendAsync(MetodoHelper.Erro, erro);
            return;
        }

        _microondasService.Iniciar(tempo, potencia);
        await Clients.Caller.SendAsync(MetodoHelper.AtualizarProgresso, _microondasService.GetStatusAtual());
    }

    public async Task Adicionar30s()
    {
        _microondasService.Adicionar30Segundos();
        await Clients.Caller.SendAsync(MetodoHelper.AtualizarProgresso, _microondasService.GetStatusAtual());
    }

    public async Task PausarOuCancelar()
    {
        var antes = _microondasService.EmAndamento;
        _microondasService.PausarOuCancelar();
        await Clients.Caller.SendAsync(MetodoHelper.AtualizarProgresso, _microondasService.GetStatusAtual());
        
        if (antes && !_microondasService.EmAndamento)
            await Clients.Caller.SendAsync(MetodoHelper.Mensagem, "Aquecimento cancelado");
        else if (antes)
            await Clients.Caller.SendAsync(MetodoHelper.Mensagem, "Aquecimento pausado");
    }
}