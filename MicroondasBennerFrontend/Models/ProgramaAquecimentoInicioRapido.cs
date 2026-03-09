using MicroondasBenner.Models.Enums;
using MicroondasBennerCommom.Models;

namespace MicroondasBennerFrontend.Models
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
