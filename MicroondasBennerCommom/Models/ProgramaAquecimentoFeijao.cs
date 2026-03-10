using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models.Base;

namespace MicroondasBennerFrontend.Models
{
    public class ProgramaAquecimentoFeijao : ProgramaAquecimento
    {
        public ProgramaAquecimentoFeijao()
        {
            Tempo = 8;
            Potencia = 9;
            TipoPrograma = ETipoPrograma.Feijao;
            SimboloProgresso = 'J';
        }
    }
}
