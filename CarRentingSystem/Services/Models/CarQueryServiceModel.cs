namespace CarRentingSystem.Services.Models
{
    public class CarQueryServiceModel
    {
        public int TotalCars { get; set; }

        public IEnumerable<CarServiceModel>? Cars { get; init; }
    }
}
