using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models.Base;

namespace MicroondasBennerCommon.Models
{
    public class ProgramaAquecimentoBoi : ProgramaAquecimento
    {
        public ProgramaAquecimentoBoi()
        {
            Tempo = 14;
            Potencia = 4;
            TipoPrograma = ETipoPrograma.Boi;
            SimboloProgresso = 'B';
        }
    }
}
