using ConsumptionRecord.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsumptionRecord.Domain.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Wait> Waits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region User

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("T_Users");
            entity.HasKey(user => user.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        #endregion

        #region Wait

        modelBuilder.Entity<Wait>(entity =>
        {
            entity.ToTable("T_Waits");
            entity.HasKey(wait => wait.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        #endregion
    }
}
