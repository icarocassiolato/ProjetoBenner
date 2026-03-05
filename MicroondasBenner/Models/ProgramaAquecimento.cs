namespace MicroondasBenner.Models;

public class ProgramaAquecimento
{
    public int Tempo { get; set; } = 30;
    public int Potencia { get; set; } = 1;
    public string StringProgresso { get; set; } = string.Empty;
    public bool EmAndamento { get; set; } = false;
    public bool Pausado { get; set; } = false;
}