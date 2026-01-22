using CarEntity = Car.Core.Domain.Car;

namespace Car.Core.Interfaces
{
    public interface ICarRepository
    {
        Task<List<CarEntity>> GetAllAsync();
        Task<CarEntity?> GetByIdAsync(Guid id);

        Task AddAsync(CarEntity car);
        Task UpdateAsync(CarEntity car);
        Task DeleteAsync(CarEntity car);

        Task SaveChangesAsync();
    }
}