using Financify.Models;
using Microsoft.EntityFrameworkCore;

namespace Financify.Data;

public class Database : DbContext
{
    public Database(DbContextOptions<Database> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasKey(c => c.Name);

        // Configurações para o relacionamento entre Transaction e Category
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Category)           // Uma transação pertence a uma categoria
            .WithMany(c => c.Transactions)     // Uma categoria pode ter muitas transações
            .HasForeignKey(t => t.CategoryName); // Chave estrangeira na tabela de transações


        modelBuilder.Entity<Transaction>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Transaction>()
            .HasAlternateKey(t => t.TransactionId);

        modelBuilder.Entity<Goal>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Goal>()
            .HasAlternateKey(t => t.GoalId);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Goal)                  // Uma Transaction tem um Goal
            .WithMany(g => g.Transactions)        // Um Goal tem muitas Transactions
            .HasForeignKey(t => t.GoalId)        // Chave estrangeira em Transaction
            .OnDelete(DeleteBehavior.SetNull);  // Restringir a exclusão em cascata

        base.OnModelCreating(modelBuilder);

    }
}