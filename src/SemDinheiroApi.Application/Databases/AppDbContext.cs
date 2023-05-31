using Microsoft.EntityFrameworkCore;
using SemDinheiroApi.Databases.Models;

namespace SemDinheiroApi.Databases;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
}