using MicroondasBenner.Models.Base;
using MicroondasBenner.Models.Enums;

namespace MicroondasBenner.Models
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
