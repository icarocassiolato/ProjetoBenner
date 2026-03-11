using MicroondasBennerAPI.Helpers;
using MicroondasBennerAPI.Models.Enums;
using MicroondasBennerAPI.Service.Services;
using MicroondasBennerAPI.Service.Contracts;
using Microsoft.AspNetCore.SignalR;

namespace MicroondasBennerAPI.Hubs;

public class MicroondasHub(
    MicroondasService microondasService, 
    IProgramasPersonalizadosService programasService
) : Hub
{
    private readonly MicroondasService _microondasService = microondasService
            ?? throw new ArgumentNullException(nameof(microondasService));
    
    private readonly IProgramasPersonalizadosService _programasService = programasService 
        ?? throw new ArgumentNullException(nameof(programasService));

    public async Task Iniciar(int tipoPrograma, string tempo = "", string potencia = "")
    {
        var programa = _microondasService.GetConfigPrograma((ETipoPrograma)tipoPrograma);

        if (programa.TipoPrograma == ETipoPrograma.Personalizado)
        {
            programa.Tempo = int.TryParse(tempo, out var t) ? t : 30;
            programa.Potencia = int.TryParse(potencia, out var p) ? p : 10;
        }

        if (!_microondasService.IsValido(programa.Tempo, programa.Potencia, out var erro))
        {
            await Clients.Caller.SendAsync(MetodoHelper.Erro, erro);
            return;
        }

        await Clients.Caller.SendAsync(MetodoHelper.Erro, "");

        _microondasService.Iniciar(programa);
        await Clients.Caller.SendAsync(MetodoHelper.AtualizarProgresso, _microondasService.GetStatusAtual());
    }

    public async Task IniciarPersonalizado(int programaId)
    {
        var programa = await _programasService.GetByIdAsync(programaId);
        if (programa == null)
        {
            await Clients.Caller.SendAsync(MetodoHelper.Erro, "Programa personalizado não encontrado.");
            return;
        }

        if (!_microondasService.IsValido(programa.Tempo, programa.Potencia, out var erro))
        {
            await Clients.Caller.SendAsync(MetodoHelper.Erro, erro);
            return;
        }

        await Clients.Caller.SendAsync(MetodoHelper.Erro, "");

        _microondasService.Iniciar(programa);
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
        else 
            if (antes)
                await Clients.Caller.SendAsync(MetodoHelper.Mensagem, "Aquecimento pausado");
    }
}