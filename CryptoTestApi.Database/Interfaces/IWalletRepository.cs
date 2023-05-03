using CryptoTestApi.Database.Models;

namespace CryptoTestApi.Database.Interfaces
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> GetWalletsAsync();
    }
}
