using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Transaction> Transactions {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(
            entityTransaction =>
            {
                entityTransaction.ToTable("transaction");
                entityTransaction.Property(transaction => transaction.Id).HasColumnName("id");
                entityTransaction.Property(transaction => transaction.Amount).HasColumnName("amount");
                entityTransaction.Property(transaction => transaction.Description).HasColumnName("description");
                entityTransaction.Property(transaction => transaction.Category).HasColumnName("category");
                entityTransaction.Property(transaction => transaction.Type).HasColumnName("type");
                entityTransaction.Property(transaction => transaction.EffectiveDate).HasColumnName("effective_date");
                entityTransaction.Property(transaction => transaction.EntryDate).HasColumnName("entry_date");
                entityTransaction.Property(transaction => transaction.Username).HasColumnName("username");
            });        
    }
}