using FirstDemo.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Infrastructure.DbContexts;

public class ApplicationDbContext : IdentityDbContext
{
    private readonly string _connectionString;
    private readonly string _migrationAssemblyName;

    public ApplicationDbContext(string connectionString, string migrationAssemblyName)
    {
        _connectionString = connectionString;
        _migrationAssemblyName = migrationAssemblyName;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString,
                b => b.MigrationsAssembly(_migrationAssemblyName)
                );
        }

        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
}
