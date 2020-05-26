using Microsoft.EntityFrameworkCore;
using YakShop.API.Models;
using YakShop.Models;


namespace YakShop.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Yak> Yaks { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}