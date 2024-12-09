using Microsoft.EntityFrameworkCore;
using SheridanNGO.Models;

public class DonationDbContext : DbContext
{
    public DonationDbContext(DbContextOptions<DonationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<NGO> NGOs { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Donation> Donations { get; set; }

    public void AddUser(User user)
    {
        this.Users.Add(user);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Define relationships
        modelBuilder.Entity<NGO>()
            .HasMany(n => n.Campaigns)
            .WithOne(c => c.NGO)
            .HasForeignKey(c => c.NGOId); 

        modelBuilder.Entity<User>()
            .HasMany(u => u.Donations)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId);
    }
}
