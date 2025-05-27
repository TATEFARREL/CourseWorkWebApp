using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CityService(
            ICityRepository cityRepository,
            ICountryRepository countryRepository,
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<CityDto> GetByIdAsync(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            return _mapper.Map<CityDto>(city);
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            var cities = await _cityRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task<IEnumerable<CityDto>> GetByCountryIdAsync(int countryId)
        {
            var cities = await _cityRepository.GetCitiesByCountryIdAsync(countryId);
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task<CityDto> CreateAsync(CityDto cityDto)
        {
            
            var countryExists = await _countryRepository.GetByIdAsync(cityDto.CountryId);
            if (countryExists == null)
            {
                throw new KeyNotFoundException($"Country with ID {cityDto.CountryId} not found");
            }

            var city = new City
            {
                Name = cityDto.Name,
                CountryId = cityDto.CountryId 
            };

            await _cityRepository.AddAsync(city);

            return new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId
            };
        }

        public async Task UpdateAsync(CityDto cityDto)
        {
            var city = await _cityRepository.GetByIdAsync(cityDto.Id);
            if (city == null)
                throw new KeyNotFoundException("City not found");

            var countryExists = await _countryRepository.GetByIdAsync(cityDto.CountryId);
            if (countryExists == null)
            {
                throw new KeyNotFoundException($"Country with ID {cityDto.CountryId} not found");
            }

            city.Name = cityDto.Name;
            city.CountryId = cityDto.CountryId;

            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteAsync(int id)
        {
            await _cityRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<CityDto>> GetAllWithDetailsAsync()
        {
            var cities = await _cityRepository.GetAllAsync();
            var cityDtos = new List<CityDto>();

            foreach (var city in cities)
            {
                var country = await _countryRepository.GetByIdAsync(city.CountryId);
                cityDtos.Add(new CityDto
                {
                    Id = city.Id,
                    Name = city.Name,
                    CountryId = city.CountryId,
                    CountryName = country?.Name ?? "Unknown"
                });
            }

            return cityDtos;
        }
    }
}