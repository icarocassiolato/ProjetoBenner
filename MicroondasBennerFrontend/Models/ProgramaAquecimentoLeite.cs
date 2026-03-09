using MicroondasBenner.Models.Enums;
using MicroondasBennerCommom.Models;

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
