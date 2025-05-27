using BLL.DTOs;

namespace BLL.Interfaces;

public interface ICountryService
{
    Task<CountryDto> GetByIdAsync(int id);
    Task<IEnumerable<CountryDto>> GetAllAsync();
    Task<CountryDto> CreateAsync(CountryDto countryDto);
    Task UpdateAsync(CountryDto countryDto);
    Task DeleteAsync(int id);
}