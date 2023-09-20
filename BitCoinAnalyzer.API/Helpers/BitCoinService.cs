using BitCoinAnalyzer.Entity;
using BitCoinAnalyzer.Service.Abstract;
using BitCoinAnalyzer.Service.Concrete;
using System.ComponentModel.DataAnnotations;

namespace BitCoinAnalyzer.API.Helpers
{
    public class BitCoinService
    {
        IDatabaseService _db;

        public BitCoinService(IDatabaseService db)
        {
            _db = db;
        }

        public IEnumerable<BitCoinChartModel> GetBitCoinDaily()
        {
            return _db.BitCoin.GetList(l => l.Timestamp > DateTime.Now.AddDays(-1))
                .Select(s => new BitCoinChartModel
                {
                Price = s.Price,
                Timestamp = $"{s.Timestamp.ToShortDateString()} : {s.Timestamp.ToShortTimeString()}",
                });
        }

        public IEnumerable<BitCoin?> GetBitCoinWeekly()
        {
            return _db.BitCoin.GetList(l => l.Timestamp > DateTime.Now.AddDays(-7))
                .GroupBy(g => g.Timestamp.Hour)
                .Select(s => s.FirstOrDefault()).AsEnumerable();
        }

        public IEnumerable<BitCoin?> GetBitCoinMonthly()
        {
            return _db.BitCoin.GetList(l => l.Timestamp > DateTime.Now.AddDays(-30))
                .GroupBy(g => g.Timestamp.Day)
                .Select(s => s.FirstOrDefault()).AsEnumerable();
        }
    }

    public class BitCoinChartModel
    {
        public decimal Price { get; set; }
        public string Timestamp { get; set; }
    }
}
