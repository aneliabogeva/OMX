using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OMX.Models;

namespace OMX.Data
{
    public class OmxDbContext : IdentityDbContext<User>
    {
      
        public OmxDbContext(DbContextOptions<OmxDbContext> options)
            : base(options)
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyFeature> PropertiesFeatures { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<Report> Reports { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(e => e.Properties)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            builder.Entity<Report>()
                .HasOne(e => e.User)
                .WithMany(e => e.Reports)
                .HasForeignKey(e => e.UserId);

            builder.Entity<PropertyFeature>()
                .HasKey(e => new { e.FeatureId, e.PropertyId });

            builder.Entity<PropertyFeature>()
                .HasOne(e => e.Feature)
                .WithMany(e => e.Properties)
                .HasForeignKey(e => e.FeatureId);

            builder.Entity<PropertyFeature>()
                .HasOne(e => e.Property)
                .WithMany(e => e.Features)
                .HasForeignKey(e => e.PropertyId);

            builder.Entity<Image>()
                .HasOne(e => e.Property)
                .WithMany(e => e.ImageNames)
                .HasForeignKey(e => e.PropertyId);

            builder.Entity<Address>()
               .HasMany(e => e.Properties)
               .WithOne(e => e.Address)
               .HasForeignKey(e => e.AddressId);


            base.OnModelCreating(builder);
        }

    }
}
