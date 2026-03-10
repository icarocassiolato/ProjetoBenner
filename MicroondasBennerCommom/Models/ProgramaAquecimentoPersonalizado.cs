using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models.Base;

namespace MicroondasBennerFrontend.Models
{
    public class ProgramaAquecimentoPersonalizado : ProgramaAquecimento
    {
        public ProgramaAquecimentoPersonalizado()
        {
            TipoPrograma = ETipoPrograma.Personalizado;
        }
    }
}
