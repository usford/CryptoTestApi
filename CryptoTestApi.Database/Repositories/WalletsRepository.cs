using CryptoTestApi.Database.Interfaces;
using CryptoTestApi.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoTestApi.Database.Repositories
{
    public class WalletsRepository : IWalletRepository
    {
        private CryptoDbContext _db;

        public WalletsRepository(CryptoDbContext db)
        {
            _db = db;
        }

        async public Task<IEnumerable<Wallet>> GetWalletsAsync()
        {
            return await _db.Wallets.ToArrayAsync();
        }
    }
}
