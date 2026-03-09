using MicroondasBennerCommom.Models;

namespace MicroondasBennerAPI.Repository.Contracts
{
    public interface IProgramasPersonalizadosRepository
    {
        Task<IEnumerable<ProgramaAquecimento>> GetAllAsync();
        Task<ProgramaAquecimento?> GetByIdAsync(int id);
        Task<int> InsertAsync(ProgramaAquecimento programa);
        Task<bool> UpdateAsync(ProgramaAquecimento programa);
        Task<bool> DeleteAsync(int id);
    }
}
