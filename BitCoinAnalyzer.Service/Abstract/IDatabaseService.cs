using BitCoinAnalyzer.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinAnalyzer.Service.Abstract
{
    public interface IDatabaseService : IDisposable
    {
        IBitCoin BitCoin { get; }
        IUser User { get; }
        int SaveChanges();
    }
}
