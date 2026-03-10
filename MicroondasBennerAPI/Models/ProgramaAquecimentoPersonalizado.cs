using MicroondasBennerAPI.Models.Base;
using MicroondasBennerAPI.Models.Enums;

namespace MicroondasBennerAPI.Models
{
    public class ProgramaAquecimentoPersonalizado : ProgramaAquecimento
    {
        public ProgramaAquecimentoPersonalizado()
        {
            TipoPrograma = ETipoPrograma.Personalizado;
        }
    }
}
