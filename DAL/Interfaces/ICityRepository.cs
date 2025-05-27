using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<IEnumerable<City>> GetCitiesByCountryIdAsync(int countryId);
        Task<IEnumerable<City>> GetCitiesWithAttractionsAsync();
        Task<IEnumerable<City>> GetAllWithDetailsAsync();
    }
}