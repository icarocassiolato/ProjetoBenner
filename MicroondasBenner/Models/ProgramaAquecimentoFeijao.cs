using MicroondasBenner.Models.Base;
using MicroondasBenner.Models.Enums;

namespace MicroondasBenner.Models
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
