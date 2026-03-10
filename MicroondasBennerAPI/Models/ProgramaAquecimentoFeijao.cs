using MicroondasBennerAPI.Models.Enums;
using MicroondasBennerAPI.Models.Base;

namespace MicroondasBennerAPI.Models
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
