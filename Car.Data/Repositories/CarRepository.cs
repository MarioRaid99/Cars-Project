using Car.Core.Interfaces;
using Car.Data.Data;
using Microsoft.EntityFrameworkCore;
using CarEntity = Car.Core.Domain.Car;

namespace Car.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDbContext _context;

        public CarRepository(CarDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarEntity>> GetAllAsync()
        {
            return await _context.Cars
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CarEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(CarEntity car)
        {
            await _context.Cars.AddAsync(car);
        }

        public Task UpdateAsync(CarEntity car)
        {
            _context.Cars.Update(car);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(CarEntity car)
        {
            _context.Cars.Remove(car);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}