using CryptoTestApi.Database.Interfaces;
using CryptoTestApi.Database.Models;
using CryptoTestApi.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Web3;
using System.Threading.Tasks;

namespace CryptoTestApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CryptoController : ControllerBase
    {
        private readonly ILogger<CryptoController> _logger;
        private Cashed _cashed;

        public CryptoController(ILogger<CryptoController> logger, 
            IWalletRepository walletsRepository,
            IServiceProvider serviceProvider,
            Cashed cashed)
        {
            _logger = logger;
            _cashed = cashed;
        }

        [HttpGet]
        public async Task<IEnumerable<Wallet>> GetWallets(int page = 1, Sorted sort = Sorted.IdDesc)
        {
            switch (sort)
            {
                case Sorted.IdAsc:
                    {
                        _cashed.Wallets = _cashed.Wallets.OrderBy(x => x.Id);
                        break;
                    }
                case Sorted.IdDesc:
                    {
                        _cashed.Wallets = _cashed.Wallets.OrderByDescending(x => x.Id);
                        break;
                    }
                case Sorted.BalanceAsc:
                    {
                        _cashed.Wallets = _cashed.Wallets.OrderBy(x => x.Balance);
                        break;
                    }
                case Sorted.BalanceDesc:
                    {
                        _cashed.Wallets = _cashed.Wallets.OrderByDescending(x => x.Balance);
                        break;
                    }
            }

            return _cashed.Wallets.Skip((page - 1) * 20).Take(20);
        }

        public enum Sorted
        {
            IdAsc,
            IdDesc,
            BalanceAsc,
            BalanceDesc
        }
    }
}