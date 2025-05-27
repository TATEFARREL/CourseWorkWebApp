using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountryService(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<CountryDto> GetByIdAsync(int id)
    {
        var country = await _countryRepository.GetByIdAsync(id);
        return _mapper.Map<CountryDto>(country);
    }

    public async Task<IEnumerable<CountryDto>> GetAllAsync()
    {
        var countries = await _countryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CountryDto>>(countries);
    }

    public async Task<CountryDto> CreateAsync(CountryDto countryDto)
    {
        var country = _mapper.Map<Country>(countryDto);
        await _countryRepository.AddAsync(country);
        return _mapper.Map<CountryDto>(country);
    }

    public async Task UpdateAsync(CountryDto countryDto)
    {
        var country = await _countryRepository.GetByIdAsync(countryDto.Id);
        if (country == null)
            throw new ArgumentException("Country not found");

        _mapper.Map(countryDto, country);
        await _countryRepository.UpdateAsync(country);
    }

    public async Task DeleteAsync(int id)
    {
        await _countryRepository.DeleteAsync(id);
    }
}