using BitCoinAnalyzer.Data.Abstract;
using BitCoinAnalyzer.Data.Concrete;
using BitCoinAnalyzer.Data.DAL;
using BitCoinAnalyzer.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinAnalyzer.Service.Concrete
{
    public class EfDatabaseService : IDatabaseService
    {
        private readonly BitCoinAnalyzerDbContext context;
        public EfDatabaseService(BitCoinAnalyzerDbContext _context) => context = _context;

        //Field
        private IBitCoin _BitCoin;
        private IUser _User;

        //Property
        public IBitCoin BitCoin => _BitCoin ??= new EFBitCoin(context);
        public IUser User => _User ??= new EFUser(context);

        public void Dispose()
           => context.Dispose();

        public int SaveChanges()
            => context.SaveChanges();
    }
}
