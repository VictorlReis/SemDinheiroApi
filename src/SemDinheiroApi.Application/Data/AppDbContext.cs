using Microsoft.EntityFrameworkCore;
using SemDinheiroApi.Databases.Models.Domain;

namespace SemDinheiroApi.Databases
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired();

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasConversion(v => v
                        .ToUniversalTime(), v => DateTime
                        .SpecifyKind(v, DateTimeKind.Utc));
                    
                entity.Property(e => e.PaymentMethod)
                    .IsRequired();

                entity.Property(e => e.Tag)
                    .IsRequired();

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 2)")
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired();
            });
        }
    }
}