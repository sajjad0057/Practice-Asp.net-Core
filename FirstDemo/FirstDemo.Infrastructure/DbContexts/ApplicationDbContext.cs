using FirstDemo.Infrastructure.Entities;
using FirstDemo.Infrastructure.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FirstDemo.Infrastructure.DbContexts;

public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
{
    private readonly string _connectionString;
    private readonly string _migrationAssemblyName;

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Result> Results { get; set; }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topic>().ToTable("Topics");
        //// Creating Composite primary key in Pivot table -
        modelBuilder.Entity<CourseRegistration>().HasKey(c => new { c.CourseId, c.StudentId });

        //// Explicitly create some relationShip : 

        modelBuilder.Entity<Course>()
            .HasMany(t => t.Topics)
            .WithOne(c => c.Course)
            .HasForeignKey(x => x.CourseId);


        modelBuilder.Entity<CourseRegistration>()
            .HasOne(c => c.Course)
            .WithMany(cs => cs.CourseStudents)
            .HasForeignKey(x => x.CourseId);


        modelBuilder.Entity<CourseRegistration>()
            .HasOne(s => s.Student)
            .WithMany(sc => sc.StudentCourses)
            .HasForeignKey(x => x.StudentId);

        modelBuilder.Entity<Student>().HasData(new StudentSeed().StudentsData);

        base.OnModelCreating(modelBuilder);
    }
}
