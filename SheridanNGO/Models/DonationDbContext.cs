using Microsoft.EntityFrameworkCore;
using SheridanNGO.Models;

public class DonationDbContext : DbContext
{
    public DonationDbContext(DbContextOptions<DonationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<NGO> NGOs { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Receipt> Receipts { get; set; }

    public void AddUser(User user)
    {
        this.Users.Add(user);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Donation>()
            .HasOne(d => d.Donor)
            .WithMany(u => u.Donations)
            .HasForeignKey(d => d.DonorID);

        modelBuilder.Entity<Donation>()
            .HasOne(d => d.NGO)
            .WithMany(n => n.Donations)
            .HasForeignKey(d => d.NGOID);

        modelBuilder.Entity<Campaign>()
            .HasOne(c => c.NGO)
            .WithMany(n => n.Campaigns)
            .HasForeignKey(c => c.NGOID);

        modelBuilder.Entity<Receipt>()
            .HasOne(r => r.Donation)
            .WithMany()
            .HasForeignKey(r => r.DonationID);
    }
}
