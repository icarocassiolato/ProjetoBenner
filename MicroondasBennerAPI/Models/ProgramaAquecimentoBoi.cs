using MicroondasBennerAPI.Models.Enums;
using MicroondasBennerAPI.Models.Base;

namespace MicroondasBennerAPI.Models
{
    public class ProgramaAquecimentoBoi : ProgramaAquecimento
    {
        public ProgramaAquecimentoBoi()
        {
            Tempo = 14;
            Potencia = 4;
            TipoPrograma = ETipoPrograma.Boi;
            SimboloProgresso = 'B';
        }
    }
}
