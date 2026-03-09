using MicroondasBennerCommon.Enums;
using MicroondasBennerCommon.Models;

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
