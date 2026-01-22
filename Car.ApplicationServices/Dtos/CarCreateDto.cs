using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.ApplicationServices.Dtos
{
    public class CarCreateDto
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
    }
}