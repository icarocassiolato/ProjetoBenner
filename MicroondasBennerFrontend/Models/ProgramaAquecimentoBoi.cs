using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models;

namespace MicroondasBennerFrontend.Models
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
