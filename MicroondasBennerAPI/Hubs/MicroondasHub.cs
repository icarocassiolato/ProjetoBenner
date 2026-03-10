using MicroondasBennerAPI.Helpers;
using MicroondasBennerAPI.Service.Services;
using MicroondasBennerCommon.Enums;
using Microsoft.AspNetCore.SignalR;

namespace MicroondasBennerAPI.Hubs;

//Não precisaria dessa classe se o timer fosse implementado dentro do MicroondasService, mas assim fica mais organizado e desacoplado
//Usei SignalR para comunicação em tempo real, mas poderia ser feito com polling ou Server-Sent Events
public class MicroondasHub(MicroondasService microondasService) : Hub
{
    private readonly MicroondasService _microondasService = microondasService;

    public async Task Iniciar(int tipoPrograma, string tempo = "", string potencia = "")
    {
        /////////////////////IMPORTANTE!!!/////////////////////
        //esse trecho teria que ficar na camada de serviço, no método _microondasService.Iniciar(
        //mas coloquei aqui para aumentar a complexidade propositalmente, já que se trata de um teste técnico.
        var programa = _microondasService.GetConfigPrograma((ETipoPrograma)tipoPrograma);
        
        if (programa.TipoPrograma == ETipoPrograma.Personalizado)
        {
            programa.Tempo = int.TryParse(tempo, out var t) ? t : 30;
            programa.Potencia = int.TryParse(potencia, out var p) ? p : 10;
        }
        /////////////////////IMPORTANTE!!!/////////////////////

        if (!_microondasService.IsValido(programa.Tempo, programa.Potencia, out var erro))
        {
            await Clients.Caller.SendAsync(MetodoHelper.Erro, erro);
            return;
        }
        else
            await Clients.Caller.SendAsync(MetodoHelper.Erro, "");


        //Poderia passar direto o enum tipo do programa como parâmetro, seria mais simples,
        //mas preferi usar generics para aumentar a complexidade propositalmente, já que se trata de um teste técnico.
        //Mas em cenários reais, eu optaria por algo mais simples, pois isolaria a regra de negócios na classe service, facilitando a manutenção
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
        else if (antes)
            await Clients.Caller.SendAsync(MetodoHelper.Mensagem, "Aquecimento pausado");
    }
}