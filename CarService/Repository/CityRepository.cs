using CarService.Models;

namespace CarService.Repository
{
    public class CityRepository: ICityRepository
    {

        CarServiceEntities context;

        public CityRepository(CarServiceEntities context)//inject
        {
            this.context = context;
        }


        public List<City> GetAllCities()
        {
            return context.City.ToList();
        }


    }
}
