using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models;

namespace MicroondasBennerFrontend.Models
{
    public class ProgramaAquecimentoFrango : ProgramaAquecimento
    {
        public ProgramaAquecimentoFrango()
        {
            Tempo = 8;
            Potencia = 7;
            TipoPrograma = ETipoPrograma.Frango;
            SimboloProgresso = 'F';
        }
    }
}
