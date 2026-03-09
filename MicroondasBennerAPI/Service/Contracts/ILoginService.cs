using MicroondasBennerCommon.Models;

namespace MicroondasBennerAPI.Service.Contracts
{
    public interface ILoginService
    {
        Task<string> UsuarioExiste(Usuario usuario);
    }
}
