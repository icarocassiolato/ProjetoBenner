using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models.Base;

namespace MicroondasBennerCommon.Models
{
    public class ProgramaAquecimentoPipoca : ProgramaAquecimento
    {
        public ProgramaAquecimentoPipoca()
        {
            Tempo = 3;
            Potencia = 7;
            TipoPrograma = ETipoPrograma.Pipoca;
            SimboloProgresso = 'P';
        }
    }
}
