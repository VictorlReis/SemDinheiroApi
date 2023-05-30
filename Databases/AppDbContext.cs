using Microsoft.EntityFrameworkCore;
using SemDinheiroApi.Databases.Models;

namespace SemDinheiroapi.Databases;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
}