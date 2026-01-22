using Car.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Car.Data.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {
        }

        public DbSet<Car.Core.Domain.Car> Cars => Set<Car.Core.Domain.Car>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car.Core.Domain.Car>(entity =>
            {
                entity.Property(x => x.Make).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Model).HasMaxLength(100).IsRequired();
                entity.Property(x => x.FuelType).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Transmission).HasMaxLength(50).IsRequired();

                entity.Property(x => x.Price).HasColumnType("decimal(18,2)");
            });
        }
    }
}