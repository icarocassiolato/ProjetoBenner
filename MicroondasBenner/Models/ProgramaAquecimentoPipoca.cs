using MicroondasBenner.Models.Base;
using MicroondasBenner.Models.Enums;

namespace MicroondasBenner.Models
{
    public class ProgramaAquecimentoPipoca : ProgramaAquecimento
    {
        public ProgramaAquecimentoPipoca()
        {
            Tempo = 3;
            Potencia = 7;
            TipoPrograma = ETipoPrograma.Pipoca;
            SimboloProgresso = 'P';
        }
    }
}
