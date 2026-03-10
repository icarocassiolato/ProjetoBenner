using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models.Base;

namespace MicroondasBennerFrontend.Models
{
    public class ProgramaAquecimentoLeite : ProgramaAquecimento
    {
        public ProgramaAquecimentoLeite()
        {
            Tempo = 5;
            Potencia = 5;
            TipoPrograma = ETipoPrograma.Leite;
            SimboloProgresso = 'L';
        }
    }
}
