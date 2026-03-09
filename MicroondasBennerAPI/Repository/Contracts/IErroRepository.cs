namespace MicroondasBennerAPI.Repository.Contracts
{
    public interface IErroRepository
    {
        Task<int> InsertAsync(string descricao);
    }
}
