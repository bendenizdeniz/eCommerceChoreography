using Microsoft.EntityFrameworkCore;

namespace StockAPI.DataStructures;

public class StockAPIDbContext : DbContext
{
    public StockAPIDbContext(DbContextOptions<StockAPIDbContext> options) : base(options)
    {
    }

    public DbSet<Stock> Stocks { get; set; }
}