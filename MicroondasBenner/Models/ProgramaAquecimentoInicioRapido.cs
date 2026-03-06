using MicroondasBenner.Models.Base;
using MicroondasBenner.Models.Enums;

namespace MicroondasBenner.Models
{
    public class ProgramaAquecimentoInicioRapido : ProgramaAquecimento
    {
        public ProgramaAquecimentoInicioRapido()
        {
            Tempo = 30;
            Potencia = 10;
            TipoPrograma = ETipoPrograma.InicioRapido;
            SimboloProgresso = 'R';
        }
    }
}
