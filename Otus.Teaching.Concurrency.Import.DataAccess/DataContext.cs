using Microsoft.EntityFrameworkCore;
using Otus.Teaching.Concurrency.Import.Core.Entities;

namespace Otus.Teaching.Concurrency.Import.DataAccess;

public class DataContext : DbContext
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=parallel;Username=postgres;Password=12345678";

    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
    }
}