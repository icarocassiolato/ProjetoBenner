using MicroondasBennerCommon.Models;

namespace MicroondasBennerAPI.Service.Contracts
{
    public interface IProgramasPersonalizadosService
    {
        Task<IEnumerable<ProgramaAquecimento>> GetAllAsync();
        Task<ProgramaAquecimento?> GetByIdAsync(int id);
        Task<int> InsertAsync(ProgramaAquecimento programa);
        Task<bool> UpdateAsync(ProgramaAquecimento programa);
        Task<bool> DeleteAsync(int id);
    }
}
