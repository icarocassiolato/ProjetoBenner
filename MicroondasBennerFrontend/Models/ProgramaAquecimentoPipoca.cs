using MicroondasBenner.Models.Enums;
using MicroondasBennerCommom.Models;

namespace MicroondasBennerFrontend.Models
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
