using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models.Base;

namespace MicroondasBennerFrontend.Models
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
