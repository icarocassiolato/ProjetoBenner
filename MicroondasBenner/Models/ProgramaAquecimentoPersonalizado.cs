using MicroondasBenner.Models.Base;
using MicroondasBenner.Models.Enums;

namespace MicroondasBenner.Models
{
    public class ProgramaAquecimentoPersonalizado : ProgramaAquecimento
    {
        public ProgramaAquecimentoPersonalizado()
        {
            TipoPrograma = ETipoPrograma.Personalizado;
        }
    }
}
