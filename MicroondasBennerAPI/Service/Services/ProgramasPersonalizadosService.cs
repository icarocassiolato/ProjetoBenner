using MicroondasBennerAPI.Repository.Contracts;
using MicroondasBennerAPI.Service.Contracts;
using MicroondasBennerCommom.Models;

namespace MicroondasBennerAPI.Service.Services
{
    public class ProgramasPersonalizadosService(IProgramasPersonalizadosRepository programasPersonalizadosRepository) : IProgramasPersonalizadosService
    {
        private readonly IProgramasPersonalizadosRepository _programasPersonalizadosRepository = programasPersonalizadosRepository;

        public Task<bool> DeleteAsync(int id)
            => _programasPersonalizadosRepository.DeleteAsync(id);

        public Task<IEnumerable<ProgramaAquecimento>> GetAllAsync()
            => _programasPersonalizadosRepository.GetAllAsync();

        public Task<ProgramaAquecimento?> GetByIdAsync(int id)
            => _programasPersonalizadosRepository.GetByIdAsync(id);

        public Task<int> InsertAsync(ProgramaAquecimento programa)
            => _programasPersonalizadosRepository.InsertAsync(programa);

        public Task<bool> UpdateAsync(ProgramaAquecimento programa)
            => _programasPersonalizadosRepository.UpdateAsync(programa);
    }
}
