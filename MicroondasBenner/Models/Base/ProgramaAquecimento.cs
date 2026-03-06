using MicroondasBenner.Models.Enums;

namespace MicroondasBenner.Models.Base;

public class ProgramaAquecimento
{
    public int Tempo { get; set; } = 30;
    public int Potencia { get; set; } = 10;
    public string StringProgresso { get; set; } = string.Empty;
    public bool EmAndamento { get; set; } = false;
    public bool Pausado { get; set; } = false;
    public ETipoPrograma TipoPrograma { get; set; } = ETipoPrograma.Padrao;
    public char SimboloProgresso { get; set; } = '.';
}