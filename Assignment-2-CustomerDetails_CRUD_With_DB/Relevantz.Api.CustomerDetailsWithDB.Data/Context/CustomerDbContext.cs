using Microsoft.EntityFrameworkCore;
using Relevantz.Api.CustomerDetailsWithDB.Data.Entities;

namespace Relevantz.Api.CustomerDetailsWithDB.Data.Context
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();
        public DbSet<CustomerBusinessProfile> CustomerBusinessProfiles => Set<CustomerBusinessProfile>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<CustomerTag> CustomerTags => Set<CustomerTag>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasIndex(e => e.TagName).IsUnique();
            });

            modelBuilder.Entity<CustomerTag>(entity =>
            {
                entity.HasKey(ct => new { ct.CustomerId, ct.TagId });

                entity.HasOne(ct => ct.Customer)
                    .WithMany(c => c.CustomerTags)
                    .HasForeignKey(ct => ct.CustomerId);

                entity.HasOne(ct => ct.Tag)
                    .WithMany(t => t.CustomerTags)
                    .HasForeignKey(ct => ct.TagId);
            });

            modelBuilder.Entity<CustomerBusinessProfile>(entity =>
            {
                entity.HasOne(bp => bp.Customer)
                    .WithOne(c => c.BusinessProfile)
                    .HasForeignKey<CustomerBusinessProfile>(bp => bp.CustomerId);
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasOne(a => a.Customer)
                    .WithMany(c => c.Addresses)
                    .HasForeignKey(a => a.CustomerId);
            });
        }
    }
}
