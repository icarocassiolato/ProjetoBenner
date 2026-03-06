using MicroondasBenner.Models.Base;
using MicroondasBenner.Models.Enums;

namespace MicroondasBenner.Models
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
