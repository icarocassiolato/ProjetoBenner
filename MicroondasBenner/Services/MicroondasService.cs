using MicroondasBenner.Models;

namespace MicroondasBenner.Services;

public class MicroondasService()
{
    public MicroondasModel Model { get; private set; } = new();

    public void Iniciar(int tempo, int potencia)
    {
        Model.Tempo = tempo;
        Model.Potencia = potencia == 0
            ? 10 
            : potencia;
        Model.Ativo = true;
        Model.Pausado = false;
        Model.BarraProcesso = string.Empty;
    }

    public void InicioRapido()
    {
        if (Model.Ativo)
            Model.Tempo += 30;
    }

    public void Pausar()
    {
        Model.Pausado = true;
        Model.Ativo = false;
    }

    public void Cancelar()
        => Model = new MicroondasModel();

    public string AtualizarBarraProgresso()
    {
        if (!Model.Ativo || Model.Tempo <= 0)
            return "Processo Finalizado";

        Model.Tempo--;
        Model.BarraProcesso += new string('.', Model.Potencia);

        return Model.BarraProcesso;
    }

    public string Validar()
    {
        if (Model.Tempo < 1 || Model.Tempo > 120)
            return "Escolha um tempo entre 1 segundo e 2 minutos";

        if (Model.Potencia < 1 || Model.Potencia > 10)
            return "Escolha uma potência entre 1 e 10";

        return string.Empty;
    }
}