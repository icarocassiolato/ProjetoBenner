using MicroondasBennerCommon.Models;

namespace MicroondasBennerAPI.Repository.Contracts
{
    public interface ILoginRepository
    {
        Task<int> UsuarioExiste(Usuario usuario);
    }
}