using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MicroondasApi.Hubs;
using MicroondasApi.Service.Contracts;
using MicroondasBenner.Services;
using MicroondasBenner.Models.Enums;
using MicroondasBenner.Helpers;

namespace MicroondasApi.Service.Services
{
    public class MicroondasAppService : IMicroondasAppService
    {
        private readonly MicroondasService _service;
        private readonly IHubContext<MicroondasHub> _hub;

        public MicroondasAppService(MicroondasService service, IHubContext<MicroondasHub> hub)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
        }

        public Task StartAsync(int tipoPrograma, int? tempo, int? potencia)
        {
            var programa = _service.GetConfigPrograma((ETipoPrograma)tipoPrograma);

            if (programa.TipoPrograma == ETipoPrograma.Personalizado)
            {
                programa.Tempo = tempo ?? programa.Tempo;
                programa.Potencia = potencia ?? programa.Potencia;
            }

            if (!_service.IsValido(programa.Tempo, programa.Potencia, out var erro))
                throw new ArgumentException(erro);

            _service.Iniciar(programa);

            return _hub.Clients.All.SendAsync(MetodoHelper.AtualizarProgresso, _service.GetStatusAtual());
        }

        public Task Add30Async()
        {
            _service.Adicionar30Segundos();
            return _hub.Clients.All.SendAsync(MetodoHelper.AtualizarProgresso, _service.GetStatusAtual());
        }

        public Task PauseOrCancelAsync()
        {
            var before = _service.EmAndamento;
            _service.PausarOuCancelar();
            var task = _hub.Clients.All.SendAsync(MetodoHelper.AtualizarProgresso, _service.GetStatusAtual());

            if (before && !_service.EmAndamento)
                return Task.WhenAll(task, _hub.Clients.All.SendAsync(MetodoHelper.Mensagem, "Aquecimento cancelado"));

            if (before)
                return Task.WhenAll(task, _hub.Clients.All.SendAsync(MetodoHelper.Mensagem, "Aquecimento pausado"));

            return task;
        }

        public Task<string> GetStatusAsync()
            => Task.FromResult(_service.GetStatusAtual());
    }
}