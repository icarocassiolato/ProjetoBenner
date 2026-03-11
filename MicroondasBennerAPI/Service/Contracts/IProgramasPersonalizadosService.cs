using MicroondasBennerAPI.Models.Base;

namespace MicroondasBennerAPI.Service.Contracts
{
    public interface IProgramasPersonalizadosService
    {
        Task<IEnumerable<ProgramaAquecimento>> GetAllAsync();
        Task<ProgramaAquecimento?> GetByIdAsync(int id);
        Task<IEnumerable<ProgramaAquecimento?>> GetByUserIdAsync(int id);
        Task<int> InsertAsync(ProgramaAquecimento programa);
        Task<bool> UpdateAsync(ProgramaAquecimento programa);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByUsuarioIdAsync(int id);
    }
}
