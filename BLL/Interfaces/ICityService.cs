using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICityService
    {
        Task<CityDto> GetByIdAsync(int id);
        Task<IEnumerable<CityDto>> GetAllAsync();
        Task<IEnumerable<CityDto>> GetByCountryIdAsync(int countryId);
        Task<CityDto> CreateAsync(CityDto cityDto);
        Task UpdateAsync(CityDto cityDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<CityDto>> GetAllWithDetailsAsync();
    }
}