using ConsumptionRecord.Data.Entities;
using ConsumptionRecord.Data.Infos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConsumptionRecord.Domain.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region User

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("T_Users");
            entity.HasKey(user => user.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedDate).HasColumnType(PostgresqlStr.ColumnTimeName);
        });

        #endregion
    }
}
