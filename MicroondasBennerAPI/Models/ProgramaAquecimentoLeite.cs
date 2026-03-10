using MicroondasBennerAPI.Models.Base;
using MicroondasBennerAPI.Models.Enums;

namespace MicroondasBennerAPI.Models
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
