using Resume_Analyzer_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Resume_Analyzer_Backend.Data;

public class DBContext: DbContext
{
    public DBContext()
    {}
    public DBContext(DbContextOptions<DBContext> options) 
        : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }
    
    public DbSet<User> User { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
        //Increment and seeding value of AdminUser primary key:
        modelBuilder.Entity<User>()
        .Property(p => p.Id)
        .UseIdentityColumn(seed: 1, increment: 1);
    }
}