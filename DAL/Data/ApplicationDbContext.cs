using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Attraction> Attractions { get; set; }
    public DbSet<Bus> Buses { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourAttraction> TourAttractions { get; set; }
    public DbSet<TourApplication> TourApplications { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Tour>()
            .Property(t => t.Price)
            .HasPrecision(18, 2);

        builder.Entity<TourAttraction>()
            .HasKey(ta => new { ta.TourId, ta.AttractionId });

        builder.Entity<TourAttraction>()
            .HasOne(ta => ta.Tour)
            .WithMany(t => t.TourAttractions)
            .HasForeignKey(ta => ta.TourId);

        builder.Entity<TourAttraction>()
            .HasOne(ta => ta.Attraction)
            .WithMany(a => a.TourAttractions)
            .HasForeignKey(ta => ta.AttractionId);
    }
}