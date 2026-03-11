using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Service.Contracts;
using MicroondasBennerAPI.Utils;
using MicroondasBennerCommon.Models;

namespace MicroondasBennerAPI.Service.Services
{
    public class UsuarioService(
        IUsuarioRepository usuarioRepository, 
        IProgramasPersonalizadosService programasPersonalizadosService
    ) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IProgramasPersonalizadosService _programasPersonalizadosService = programasPersonalizadosService;

        public async Task<bool> DeleteAsync(int id)
        {
            await _programasPersonalizadosService.DeleteByUsuarioIdAsync(id);
            return await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
            => await _usuarioRepository.GetAllAsync();

        public async Task<Usuario?> GetByIdAsync(int id)
            => await _usuarioRepository.GetByIdAsync(id);

        public async Task<int> InsertAsync(Usuario usuario)
        {
            ValidarCamposVazios(usuario);

            usuario.Senha = HashUtils.GerarSha1(usuario.Senha);
            return await _usuarioRepository.InsertAsync(usuario);
        }

        private static void ValidarCamposVazios(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nome))
                throw new ApplicationException("O nome é obrigatório.");

            if (string.IsNullOrEmpty(usuario.Senha))
                throw new ApplicationException("A senha é obrigatória.");
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            ValidarCamposVazios(usuario);

            usuario.Senha = HashUtils.GerarSha1(usuario.Senha);
            return await _usuarioRepository.UpdateAsync(usuario);
        }
    }
}
