using CryptoTestApi.Controllers;
using CryptoTestApi.Database.Interfaces;
using CryptoTestApi.Database.Models;
using Nethereum.Web3;

namespace CryptoTestApi.BackgroundServices
{
    public class CheckBalancesService : BackgroundService
    {
        private readonly ILogger<CheckBalancesService> _logger;
        private Web3 _web3;
        private IServiceProvider _serviceProvider;
        private Cashed _cashed;

        public CheckBalancesService(ILogger<CheckBalancesService> logger,
            IServiceProvider serviceProvider,
            Cashed cashed,
            IConfiguration config)
        {
            _logger = logger;
            _web3 = new Web3(config["UrlTestnet"]);
            _serviceProvider = serviceProvider;
            _cashed = cashed;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning("Загрузка начальных данных...");

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var walletsRepository = scope.ServiceProvider.GetRequiredService<IWalletRepository>();

                var wallets = await walletsRepository.GetWalletsAsync();
                List<Task> tasks = new List<Task>();

                foreach (var wallet in wallets)
                {
                    var t = Task.Run(async () =>
                    {
                        var hexBalance = await _web3.Eth.GetBalance.SendRequestAsync(wallet.Address);
                        wallet.Balance = Math.Round(Web3.Convert.FromWei(hexBalance.Value), 2);
                    });

                    tasks.Add(t);
                }
                
                Task.WaitAll(tasks.ToArray());

                _logger.LogWarning("Данные обновлены");

                _cashed.Wallets = wallets;

                await Task.Delay(10000);
            }
        }
    }
}
