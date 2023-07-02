using CarService.Models;

namespace CarService.Repository
{
    public interface ICityRepository
    {
        public List<City> GetAllCities();
    }
}