using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext(options)
{
    public DbSet<ProjectEntity> Project { get; set; }
    public DbSet<UserEntity> User { get; set; }
    public DbSet<MemberEntity> Member { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ProjectEntity>()
            .Property(p => p.Price)
            .HasColumnType("REAL");
        
        modelBuilder.Entity<ProjectEntity>()
            .Property(o => o.DateCreated)
            .HasColumnType("TEXT");
        
        modelBuilder.Entity<ProjectEntity>()
            .Property(o => o.DateDue)
            .HasColumnType("TEXT");
        
    }
}