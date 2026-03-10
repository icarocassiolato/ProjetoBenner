using System.Threading.Tasks;

namespace MicroondasApi.Service.Contracts
{
    public interface IMicroondasAppService
    {
        Task StartAsync(int tipoPrograma, int? tempo, int? potencia);
        Task Add30Async();
        Task PauseOrCancelAsync();
        Task<string> GetStatusAsync();
    }
}