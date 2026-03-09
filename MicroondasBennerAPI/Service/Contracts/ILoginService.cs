namespace MicroondasBennerAPI.Service.Contracts
{
    public interface ILoginService
    {
        string GerarToken(string username);
    }
}
