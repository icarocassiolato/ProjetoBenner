using MicroondasBenner.Helpers;
using MicroondasBenner.Hubs;
using MicroondasBenner.Models.Enums;
using MicroondasBennerCommom.Models;
using MicroondasBennerFrontend.Models;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MicroondasBennerFrontend.Services;

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
    private readonly Timer _timer = new(TimeSpan.FromSeconds(1)); //Mais legível que new(1000)

    public MicroondasService(IHubContext<MicroondasHub> hubContext)
    {
        _hubContext = hubContext 
            ?? throw new ArgumentNullException(nameof(hubContext));

        _timer.Elapsed += OnTimer;
        _timer.AutoReset = true;
    }

    private void OnTimer(object? sender, ElapsedEventArgs e)
        => Timer();

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
        _programa.StringProgresso += new string(_programa.SimboloProgresso, pontosPorSegundo);

        _programa.Tempo = tempoRestante;

        EnviarAtualizacao();
    }

    //Poderia passar direto o enum tipo do programa como parâmetro, seria mais simples,
    //mas preferi usar generics para aumentar a complexidade propositalmente, já que se trata de um teste técnico.
    //Mas em cenários reais, eu optaria por algo mais simples, pois isolaria a regra de negócios na classe service, facilitando a manutenção
    public void Iniciar<T>(T programa) where T : ProgramaAquecimento 
    {
        if (EmAndamento && Pausado)
        {
            _programa.Pausado = false;
            _stopwatch.Start();
            _timer.Start();
            EnviarAtualizacao();
            return;
        }

        if (EmAndamento)
            return;

        _programa = programa;
        if (!IsValido(_programa.Tempo, _programa.Potencia, out var erro))
            throw new ArgumentException(erro);

        _programa.EmAndamento = true;

        _tempoInicial = _programa.Tempo;
        _stopwatch.Restart();
        _timer.Start();

        EnviarAtualizacao();
    }

    public void Adicionar30Segundos()
    {
        if (EmAndamento && !Pausado)
        {
            _tempoInicial += 30;
            _programa.Tempo += 30;
            EnviarAtualizacao();
        }
    }

    public void Pausar()
    {
        if (EmAndamento && !Pausado)
        {
            _programa.Pausado = true;
            _stopwatch.Stop();
            _timer.Stop();
            EnviarAtualizacao();
        }
    }

    public void Cancelar()
    {
        _programa = new ProgramaAquecimento();
        _stopwatch.Reset();
        _timer.Stop();
        EnviarAtualizacao();
    }

    private void Finalizar()
    {
        _programa.EmAndamento = false;
        _programa.StringProgresso += "\nAquecimento concluído";
        _stopwatch.Stop();
        _timer.Stop();
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
            return _programa.StringProgresso + $"\n(Pausado - {tempoFormatado} restantes)";

        return _programa.StringProgresso + $"\n({tempoFormatado} restantes)";
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
            _timer.Stop();
            EnviarAtualizacao();
        }
        else if (Pausado)
        {
            // Cancelar (se já pausado)
            _programa = new ProgramaAquecimento();
            _stopwatch.Reset();
            _timer.Stop();
            EnviarAtualizacao();
        }
    }

    public void Dispose()
    {
        _timer.Stop();
        _timer.Dispose();
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