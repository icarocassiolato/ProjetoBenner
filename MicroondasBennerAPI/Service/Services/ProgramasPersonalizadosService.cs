using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Service.Contracts;
using MicroondasBennerCommon.Models;

namespace MicroondasBennerAPI.Service.Services
{
    public class ProgramasPersonalizadosService(IProgramasPersonalizadosRepository programasPersonalizadosRepository) : IProgramasPersonalizadosService
    {
        private readonly IProgramasPersonalizadosRepository _programasPersonalizadosRepository = programasPersonalizadosRepository;

        public async Task<bool> DeleteAsync(int id)
            => await _programasPersonalizadosRepository.DeleteAsync(id);

        public async Task<IEnumerable<ProgramaAquecimento>> GetAllAsync()
            => await _programasPersonalizadosRepository.GetAllAsync();

        public async Task<ProgramaAquecimento?> GetByIdAsync(int id)
            => await _programasPersonalizadosRepository.GetByIdAsync(id);

        public async Task<int> InsertAsync(ProgramaAquecimento programa)
            => await _programasPersonalizadosRepository.InsertAsync(programa);

        public async Task<bool> UpdateAsync(ProgramaAquecimento programa)
            => await _programasPersonalizadosRepository.UpdateAsync(programa);
    }
}
