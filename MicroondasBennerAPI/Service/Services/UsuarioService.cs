using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Service.Contracts;
using MicroondasBennerAPI.Utils;
using MicroondasBennerCommon.Models;

namespace MicroondasBennerAPI.Service.Services
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<bool> DeleteAsync(int id)
            => await _usuarioRepository.DeleteAsync(id);

        public async Task<IEnumerable<Usuario>> GetAllAsync()
            => await _usuarioRepository.GetAllAsync();

        public async Task<Usuario?> GetByIdAsync(int id)
            => await _usuarioRepository.GetByIdAsync(id);

        public async Task<int> InsertAsync(Usuario usuario)
        {
            usuario.Senha = HashUtils.GerarSha1(usuario.Senha);
            return await _usuarioRepository.InsertAsync(usuario);
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            usuario.Senha = HashUtils.GerarSha1(usuario.Senha);
            return await _usuarioRepository.UpdateAsync(usuario);
        }
    }
}
