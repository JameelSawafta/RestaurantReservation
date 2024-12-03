using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Domain.Entities;

namespace RestaurantReservation.Db.DbContext;

public class RestaurantReservationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    
    public RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Customer>()
            .Property(c => c.CustomerId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<Reservation>()
            .Property(r => r.ReservationId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<Order>()
            .Property(o => o.OrderId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<OrderItem>()
            .Property(o => o.OrderItemId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<Employee>()
            .Property(e => e.EmployeeId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<Table>()
            .Property(t => t.TableId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<Restaurant>()
            .Property(r=> r.RestaurantId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<MenuItem>()
            .Property(m => m.ItemId)
            .HasDefaultValueSql("gen_random_uuid()");
    }
}