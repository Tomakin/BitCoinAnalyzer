using BitCoinAnalyzer.Core.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinAnalyzer.Entity
{
    public class BitCoin : BaseEntity, IEntity
    {
        public decimal Price { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
