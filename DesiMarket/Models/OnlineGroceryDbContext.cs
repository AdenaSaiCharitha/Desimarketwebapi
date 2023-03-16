using Microsoft.EntityFrameworkCore;

namespace DesiMarket.Models
{
    public class OnlineGroceryDbContext : DbContext
    {
        public OnlineGroceryDbContext(DbContextOptions<OnlineGroceryDbContext> options)
          : base(options)
        {
        }
        public  DbSet<Users> Users { get; set; }
        public  DbSet<Products> Product { get; set; }
        public  DbSet<Orders> Order { get; set; }
    }
}
