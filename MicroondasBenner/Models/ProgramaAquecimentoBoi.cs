using MicroondasBenner.Models.Base;
using MicroondasBenner.Models.Enums;

namespace MicroondasBenner.Models
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
