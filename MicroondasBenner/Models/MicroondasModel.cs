namespace MicroondasBenner.Models;

public class MicroondasModel()
{
    public int Tempo { get; set; } = 0;
    public int Potencia { get; set; } = 10;
    public string BarraProcesso { get; set; } = String.Empty;
    public bool Ativo { get; set; } = false;
    public bool Pausado { get; set; } = false;
}