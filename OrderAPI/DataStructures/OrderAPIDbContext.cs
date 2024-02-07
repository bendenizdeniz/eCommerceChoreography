using Microsoft.EntityFrameworkCore;

namespace eCommerceChoreography.DataStructures;

public class OrderAPIDbContext : DbContext
{
    public OrderAPIDbContext(DbContextOptions<OrderAPIDbContext> options) : base(options)
    {
    }


    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }
}