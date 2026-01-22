using Car.ApplicationServices.Dtos;
using Car.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllAsync();
            return View(cars);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car is null) return NotFound();
            return View(car);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CarCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            try
            {
                await _carService.CreateAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car is null) return NotFound();

            var vm = new CarUpdateDto
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                Mileage = car.Mileage,
                FuelType = car.FuelType,
                Transmission = car.Transmission
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarUpdateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            try
            {
                var updated = await _carService.UpdateAsync(dto);
                if (!updated) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car is null) return NotFound();
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deleted = await _carService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}