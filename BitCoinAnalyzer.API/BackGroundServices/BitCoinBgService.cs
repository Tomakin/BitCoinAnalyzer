using BitCoinAnalyzer.Data.DAL;
using BitCoinAnalyzer.Entity;
using System.Text.Json;

namespace BitCoinAnalyzer.API.BackGroundServices
{
    public class BitCoinBgService : BackgroundService
    {
        private readonly ILogger<BitCoinBgService> _logger;
        private readonly IServiceProvider _services;

        public BitCoinBgService(
            ILogger<BitCoinBgService> logger,
            IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<BitCoinAnalyzerDbContext>();
                    var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();

                    try
                    {
                        var response = await httpClient.GetAsync("https://api.coindesk.com/v1/bpi/currentprice.json");

                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var bitcoinData = JsonSerializer.Deserialize<BitCoinModel>(content);
                            var bitcoinPrice = new BitCoin
                            {
                                Price = bitcoinData.bpi.USD.rate_float,
                                Timestamp = DateTime.Parse(bitcoinData.time.updatedISO).ToLocalTime(),
                            };

                            dbContext.BitCoins.Add(bitcoinPrice);
                            await dbContext.SaveChangesAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Bitcoin verileri çekilirken bir hata oluştu.");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }


        }
    }

    class BitCoinModel
    {
        public bpi bpi { get; set; }
        public time time { get; set; }
    }
    class bpi
    {
        public USD USD { get; set; }
    }
    class time
    {
        public string updatedISO { get; set; }
    }
    class USD
    {
        public decimal rate_float { get; set; }
    }
}
