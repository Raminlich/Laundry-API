using LaundryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LaundryAPI.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<AddressLine> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressLine>()
                .HasOne<UserProfile>()
                .WithMany(a => a.AddressLines)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Order>()
                .HasOne<UserProfile>()
                .WithMany(a => a.Orders)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.PickUpAddress)
                .WithMany()
                .HasForeignKey(o => o.PickUpAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryAddress)
                .WithMany()
                .HasForeignKey(o => o.DeliveryAddressId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
