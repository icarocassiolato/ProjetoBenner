using MicroondasBennerAPI.Helpers;
using MicroondasBennerAPI.Hubs;
using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models;
using MicroondasBennerCommon.Models.Base;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace MicroondasBennerAPI.Service.Services;

public class MicroondasService : IDisposable
{
    private readonly IHubContext<MicroondasHub> _hubContext;

    private ProgramaAquecimento _programa { get; set; } = new();

    public bool EmAndamento //Para deixar a variável Programa privada, evitando o uso externo
        => _programa.EmAndamento;

    public bool Pausado //Para deixar a variável Programa privada, evitando o uso externo
        => _programa.Pausado;

    private Stopwatch _stopwatch = new();
    private int _tempoInicial;

    public MicroondasService(IHubContext<MicroondasHub> hubContext)
    {
        _hubContext = hubContext 
            ?? throw new ArgumentNullException(nameof(hubContext));
    }

    // Chamado pelo MicroondasBackgroundService a cada segundo
    public void Timer()
    {
        if (!EmAndamento || Pausado)
            return;

        var segundosDecorridos = (int)_stopwatch.Elapsed.TotalSeconds;
        var tempoRestante = _tempoInicial - segundosDecorridos;

        if (tempoRestante <= 0)
        {
            Finalizar();
            return;
        }

        int pontosPorSegundo = _programa.Potencia;
        _programa.StringProgresso += $" {new string(_programa.SimboloProgresso, pontosPorSegundo)}";

        _programa.Tempo = tempoRestante;

        EnviarAtualizacao();
    }

    public void Iniciar<T>(T programa) where T : ProgramaAquecimento 
    {
        if (EmAndamento && Pausado)
        {
            _programa.Pausado = false;
            _stopwatch.Start();
            EnviarAtualizacao();
            return;
        }

        if (EmAndamento && !Pausado)
        {
            Adicionar30Segundos();
            return;
        }

        _programa = programa;
        if (!IsValido(_programa.Tempo, _programa.Potencia, out var erro))
            throw new ArgumentException(erro);

        _programa.EmAndamento = true;

        _tempoInicial = _programa.Tempo;
        _stopwatch.Restart();

        EnviarAtualizacao();
    }

    public void Adicionar30Segundos()
    {
        if (!EmAndamento 
            || Pausado 
            || _programa.Tempo > 90 
            || (_programa.TipoPrograma is not ETipoPrograma.Personalizado and not ETipoPrograma.Padrao))
            return;

        _tempoInicial += 30;
        _programa.Tempo += 30;
        EnviarAtualizacao();
    }

    public void Pausar()
    {
        if (!EmAndamento || Pausado)
            return;

        _programa.Pausado = true;
        _stopwatch.Stop();
        EnviarAtualizacao();
    }

    public void Cancelar()
    {
        _programa = new ProgramaAquecimento();
        _stopwatch.Reset();
        EnviarAtualizacao();
    }

    private void Finalizar()
    {
        _programa.EmAndamento = false;
        _programa.StringProgresso += "\nAquecimento concluído";
        _stopwatch.Stop();
        EnviarAtualizacao();
    }

    private async void EnviarAtualizacao()
    {
        var status = GetStatusAtual();
        await _hubContext.Clients.All.SendAsync(MetodoHelper.AtualizarProgresso, status);
    }

    private string FormatarTempo(int segundosTotais)
    {
        if (segundosTotais < 60)
            return $"{segundosTotais}s";

        int minutos = segundosTotais / 60;
        int segundos = segundosTotais % 60;

        if (segundos == 0)
            return $"{minutos}m";

        return $"{minutos}m e {segundos}s";
    }

    public string GetStatusAtual()
    {
        if (!EmAndamento)
            return _programa.StringProgresso;

        var tempoFormatado = FormatarTempo(_programa.Tempo);

        if (Pausado)
            return $"{_programa.StringProgresso}\n(Pausado - {tempoFormatado} restantes)";

        return $"{_programa.StringProgresso} \n({tempoFormatado} restantes)";
    }

    public bool IsValido(int tempo, int potencia, out string erro)
    {
        erro = string.Empty;
        if (tempo < 1 || tempo > 120)
            erro += "Tempo inválido (1 a 120 segundos). ";

        if (potencia < 1 || potencia > 10)
            erro += "Potência inválida (1 a 10).";

        return string.IsNullOrWhiteSpace(erro);
    }

    public void PausarOuCancelar()
    {
        if (EmAndamento && !Pausado)
        {
            // Pausar
            _programa.Pausado = true;
            _stopwatch.Stop();
            EnviarAtualizacao();
        }
        else if (Pausado)
        {
            // Cancelar se já pausado
            _programa = new ProgramaAquecimento();
            _stopwatch.Reset();
            EnviarAtualizacao();
        }
    }

    public void Dispose()
    {
        _stopwatch.Reset();
    }

    public ProgramaAquecimento GetConfigPrograma(ETipoPrograma tipoPrograma) 
        => tipoPrograma switch
        {
            ETipoPrograma.Personalizado => new ProgramaAquecimentoPersonalizado(),
            ETipoPrograma.Padrao => new ProgramaAquecimento(),
            ETipoPrograma.InicioRapido => new ProgramaAquecimentoInicioRapido(),
            ETipoPrograma.Pipoca => new ProgramaAquecimentoPipoca(),
            ETipoPrograma.Leite => new ProgramaAquecimentoLeite(),
            ETipoPrograma.Boi => new ProgramaAquecimentoBoi(),
            ETipoPrograma.Frango => new ProgramaAquecimentoFrango(),
            ETipoPrograma.Feijao => new ProgramaAquecimentoFeijao(),
            _ => new ProgramaAquecimento(),
        };
}