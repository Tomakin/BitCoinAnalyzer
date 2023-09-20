using BitCoinAnalyzer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoinAnalyzer.Data.DAL
{
    public class BitCoinAnalyzerDbContext : DbContext
    {
        public BitCoinAnalyzerDbContext(DbContextOptions<BitCoinAnalyzerDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }


        public DbSet<BitCoin> BitCoins { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasKey(k => k.ID);


            modelBuilder.Entity<BitCoin>().HasKey(k => k.ID);


            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    ID = 1,
                    CreatedDate = DateTime.Now,
                    Username = "test",
                    Password = "testPass",
                });
        }
    }
}
