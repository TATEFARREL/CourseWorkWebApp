using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces;

public interface ICountryRepository : IRepository<Country>
{
    Task<IEnumerable<Country>> GetCountriesWithCitiesAsync();
}