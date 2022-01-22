using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
    }

    public DbSet<User> Users {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("user");
        modelBuilder.Entity<User>().Property(user => user.Id).ValueGeneratedOnAdd();
    }
}