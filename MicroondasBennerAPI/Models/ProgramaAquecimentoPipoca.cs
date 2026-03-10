using MicroondasBennerAPI.Models.Enums;
using MicroondasBennerAPI.Models.Base;

namespace MicroondasBennerAPI.Models
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
