using CarRentingSystem.Data;
using CarRentingSystem.Services.Statistics;

namespace CarRentingSystem.Services.Models
{
    public class StatisticsService : IStatisticsService
    {
        private readonly CarRentalDbContext data;

        public StatisticsService(CarRentalDbContext data)
        {
            this.data = data;
        }
        public StatisticsServiceModel Total()
        {
            var totalCars = this.data.Cars.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalCars = totalCars,
                TotalUsers = totalUsers,
                TotalRents = 0
            };
        }

    }
}
