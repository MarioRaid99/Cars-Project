using Car.ApplicationServices.Dtos;
using Car.ApplicationServices.Services;
using Car.Data.Data;
using Car.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Car.CarTest
{
    public class CarServiceTests
    {
        private static CarService CreateService()
        {
            var options = new DbContextOptionsBuilder<CarDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new CarDbContext(options);
            var repo = new CarRepository(context);

            return new CarService(repo);
        }

        [Fact]
        public async Task CreateAsync_SetsCreatedAtAndModifiedAt()
        {
            var service = CreateService();

            var dto = new CarCreateDto
            {
                Make = "Toyota",
                Model = "Corolla",
                Year = 2020,
                Price = 15000,
                Mileage = 80000,
                FuelType = "Petrol",
                Transmission = "Automatic"
            };

            var result = await service.CreateAsync(dto);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.NotEqual(default, result.CreatedAt);
            Assert.NotEqual(default, result.ModifiedAt);
            Assert.True(result.ModifiedAt >= result.CreatedAt);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesFieldsAndModifiedAt()
        {
            var service = CreateService();

            var created = await service.CreateAsync(new CarCreateDto
            {
                Make = "BMW",
                Model = "320",
                Year = 2018,
                Price = 20000,
                Mileage = 120000,
                FuelType = "Diesel",
                Transmission = "Automatic"
            });

            var oldModifiedAt = created.ModifiedAt;

            var updated = await service.UpdateAsync(new CarUpdateDto
            {
                Id = created.Id,
                Make = "BMW",
                Model = "320",
                Year = 2018,
                Price = 19000,
                Mileage = 115000,
                FuelType = "Diesel",
                Transmission = "Automatic"
            });

            Assert.True(updated);

            var after = await service.GetByIdAsync(created.Id);
            Assert.NotNull(after);
            Assert.Equal(19000, after!.Price);
            Assert.Equal(115000, after.Mileage);
            Assert.True(after.ModifiedAt > oldModifiedAt);
        }

        [Fact]
        public async Task CreateAsync_Throws_WhenYearIsInvalid()
        {
            var service = CreateService();

            var dto = new CarCreateDto
            {
                Make = "Ford",
                Model = "Focus",
                Year = 1700,
                Price = 5000,
                Mileage = 100000,
                FuelType = "Petrol",
                Transmission = "Manual"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(dto));
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            var service = CreateService();

            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }
    }
}
