using Car.ApplicationServices.Dtos;

namespace Car.ApplicationServices.Interfaces
{
    public interface ICarService
    {
        Task<List<CarDto>> GetAllAsync();
        Task<CarDto?> GetByIdAsync(Guid id);
        Task<CarDto> CreateAsync(CarCreateDto dto);
        Task<bool> UpdateAsync(CarUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}