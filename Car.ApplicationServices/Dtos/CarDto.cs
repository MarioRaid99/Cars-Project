namespace Car.ApplicationServices.Dtos
{
    public class CarDto
    {
        public System.Guid Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string FuelType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;

        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ModifiedAt { get; set; }
    }
}