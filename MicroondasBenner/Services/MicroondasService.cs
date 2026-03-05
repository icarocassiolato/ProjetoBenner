using MicroondasBenner.Helpers;
using MicroondasBenner.Hubs;
using MicroondasBenner.Models;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MicroondasBenner.Services;

public class MicroondasService : IDisposable
{
    private readonly IHubContext<MicroondasHub> _hubContext;

    private ProgramaAquecimento Programa { get; set; } = new();

    public bool EmAndamento //Para deixar a variável Programa privada, evitando o uso externo
        => Programa.EmAndamento;

    public bool Pausado //Para deixar a variável Programa privada, evitando o uso externo
        => Programa.Pausado;

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

        int pontosPorSegundo = Programa.Potencia;
        Programa.StringProgresso += new string('.', pontosPorSegundo);

        Programa.Tempo = tempoRestante;

        EnviarAtualizacao();
    }

    public void Iniciar(int tempoSegundos, int potencia)
    {
        if (!IsValido(tempoSegundos, potencia, out var erro))
            throw new ArgumentException(erro);

        Programa = new ProgramaAquecimento
        {
            Tempo = tempoSegundos,
            Potencia = potencia,
            StringProgresso = string.Empty,
            EmAndamento = true,
            Pausado = false
        };

        _tempoInicial = tempoSegundos;
        _stopwatch.Restart();
        _timer.Start();

        EnviarAtualizacao();
    }

    public void Adicionar30Segundos()
    {
        if (EmAndamento && !Pausado)
        {
            _tempoInicial += 30;
            Programa.Tempo += 30;
            EnviarAtualizacao();
        }
    }

    public void Pausar()
    {
        if (EmAndamento && !Pausado)
        {
            Programa.Pausado = true;
            _stopwatch.Stop();
            _timer.Stop();
            EnviarAtualizacao();
        }
    }

    public void Cancelar()
    {
        Programa = new ProgramaAquecimento();
        _stopwatch.Reset();
        _timer.Stop();
        EnviarAtualizacao();
    }

    private void Finalizar()
    {
        Programa.EmAndamento = false;
        Programa.StringProgresso += "\nAquecimento concluído";
        _stopwatch.Stop();
        _timer.Stop();
        EnviarAtualizacao();
    }

    private async void EnviarAtualizacao()
    {
        var status = GetStatusAtual();
        await _hubContext.Clients.All.SendAsync(MetodoHelper.AtualizarProgresso, status);
    }

    public string GetStatusAtual()
    {
        if (!EmAndamento)
            return Programa.StringProgresso;

        if (Pausado)
            return Programa.StringProgresso + $"\n(Pausado - {Programa.Tempo}s restantes)";

        return Programa.StringProgresso + $"   ({Programa.Tempo}s restantes)";
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
            Programa.Pausado = true;
            _stopwatch.Stop();
            _timer.Stop();
            EnviarAtualizacao();
        }
        else if (Pausado)
        {
            // Cancelar (se já pausado)
            Programa = new ProgramaAquecimento();
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
}