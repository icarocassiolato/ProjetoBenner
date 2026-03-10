using System.Threading.Tasks;
using MicroondasBennerAPI.Repository.Contracts;

namespace MicroondasBennerAPI.Tests;

public class NoOpErroRepository : IErroRepository
{
    public Task<int> InsertAsync(string descricao)
    {
        return Task.FromResult(0);
    }
}