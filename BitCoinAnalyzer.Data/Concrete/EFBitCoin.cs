using BitCoinAnalyzer.Core.Data;
using BitCoinAnalyzer.Data.Abstract;
using BitCoinAnalyzer.Data.DAL;
using BitCoinAnalyzer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinAnalyzer.Data.Concrete
{
    public class EFBitCoin : EFCoreRepository<BitCoin>, IBitCoin  
    {
        public EFBitCoin(BitCoinAnalyzerDbContext context) : base(context) { }
    }
}
