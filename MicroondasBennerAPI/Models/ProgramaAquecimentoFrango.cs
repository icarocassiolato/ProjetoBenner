using MicroondasBennerAPI.Models.Enums;
using MicroondasBennerAPI.Models.Base;

namespace MicroondasBennerAPI.Models
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
