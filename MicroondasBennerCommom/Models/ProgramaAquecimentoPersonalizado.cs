using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models.Base;

namespace MicroondasBennerCommon.Models
{
    public class ProgramaAquecimentoPersonalizado : ProgramaAquecimento
    {
        public ProgramaAquecimentoPersonalizado()
        {
            TipoPrograma = ETipoPrograma.Personalizado;
        }
    }
}
