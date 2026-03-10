using MicroondasBennerAPI.Models.Enums;
using MicroondasBennerAPI.Models.Base;

namespace MicroondasBennerAPI.Models
{
    public class ProgramaAquecimentoInicioRapido : ProgramaAquecimento
    {
        public ProgramaAquecimentoInicioRapido()
        {
            Tempo = 30;
            Potencia = 10;
            TipoPrograma = ETipoPrograma.InicioRapido;
            SimboloProgresso = 'R';
        }
    }
}
