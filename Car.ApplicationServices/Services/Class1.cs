using Car.ApplicationServices.Dtos;
using Car.ApplicationServices.Interfaces;
using Car.Core.Interfaces;
using CarEntity = Car.Core.Domain.Car;

namespace Car.ApplicationServices.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<CarDto>> GetAllAsync()
        {
            var cars = await _carRepository.GetAllAsync();
            return cars.Select(MapToDto).ToList();
        }

        public async Task<CarDto?> GetByIdAsync(Guid id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            return car is null ? null : MapToDto(car);
        }

        public async Task<CarDto> CreateAsync(CarCreateDto dto)
        {
            Validate(dto);

            var now = DateTime.UtcNow;

            var car = new CarEntity
            {
                Make = dto.Make.Trim(),
                Model = dto.Model.Trim(),
                Year = dto.Year,
                Price = dto.Price,
                Mileage = dto.Mileage,
                FuelType = dto.FuelType.Trim(),
                Transmission = dto.Transmission.Trim(),
                CreatedAt = now,
                ModifiedAt = now
            };

            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();

            return MapToDto(car);
        }

        public async Task<bool> UpdateAsync(CarUpdateDto dto)
        {
            Validate(dto);

            var car = await _carRepository.GetByIdAsync(dto.Id);
            if (car is null) return false;

            car.Make = dto.Make.Trim();
            car.Model = dto.Model.Trim();
            car.Year = dto.Year;
            car.Price = dto.Price;
            car.Mileage = dto.Mileage;
            car.FuelType = dto.FuelType.Trim();
            car.Transmission = dto.Transmission.Trim();
            car.ModifiedAt = DateTime.UtcNow;

            await _carRepository.UpdateAsync(car);
            await _carRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car is null) return false;

            await _carRepository.DeleteAsync(car);
            await _carRepository.SaveChangesAsync();

            return true;
        }

        private static void Validate(CarCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Make))
                throw new ArgumentException("Make is required.");

            if (string.IsNullOrWhiteSpace(dto.Model))
                throw new ArgumentException("Model is required.");

            var currentYear = DateTime.UtcNow.Year;
            if (dto.Year < 1886 || dto.Year > currentYear + 1)
                throw new ArgumentException("Year is out of allowed range.");

            if (dto.Price < 0)
                throw new ArgumentException("Price cannot be negative.");

            if (dto.Mileage < 0)
                throw new ArgumentException("Mileage cannot be negative.");

            if (string.IsNullOrWhiteSpace(dto.FuelType))
                throw new ArgumentException("FuelType is required.");

            if (string.IsNullOrWhiteSpace(dto.Transmission))
                throw new ArgumentException("Transmission is required.");
        }

        private static CarDto MapToDto(CarEntity car)
        {
            return new CarDto
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                Mileage = car.Mileage,
                FuelType = car.FuelType,
                Transmission = car.Transmission,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };
        }
    }
}