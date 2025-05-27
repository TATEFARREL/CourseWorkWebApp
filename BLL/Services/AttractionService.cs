using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AttractionService : IAttractionService
    {
        private readonly IAttractionRepository _attractionRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public AttractionService(
            IAttractionRepository attractionRepository,
            ICityRepository cityRepository,
            IMapper mapper)
        {
            _attractionRepository = attractionRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<AttractionDto> GetByIdAsync(int id)
        {
            var attraction = await _attractionRepository.GetByIdAsync(id);
            return _mapper.Map<AttractionDto>(attraction);
        }

        public async Task<IEnumerable<AttractionDto>> GetAllAsync()
        {
            var attractions = await _attractionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AttractionDto>>(attractions);
        }

        public async Task<IEnumerable<AttractionDto>> GetByCityIdAsync(int cityId)
        {
            var attractions = await _attractionRepository.GetByCityIdAsync(cityId);
            return _mapper.Map<IEnumerable<AttractionDto>>(attractions);
        }

        public async Task<AttractionDto> CreateAsync(AttractionDto attractionDto)
        {
            var city = await _cityRepository.GetByIdAsync(attractionDto.CityId);
            if (city == null)
                throw new KeyNotFoundException($"City with ID {attractionDto.CityId} not found");

            var attraction = new Attraction
            {
                Name = attractionDto.Name,
                Description = attractionDto.Description,
                CityId = attractionDto.CityId
            };

            await _attractionRepository.AddAsync(attraction);

            return new AttractionDto
            {
                Id = attraction.Id,
                Name = attraction.Name,
                Description = attraction.Description,
                CityId = attraction.CityId,
                CityName = city.Name
            };
        }

        public async Task UpdateAsync(AttractionDto attractionDto)
        {
            var attraction = await _attractionRepository.GetByIdAsync(attractionDto.Id);
            if (attraction == null)
                throw new KeyNotFoundException("Attraction not found");

            var cityExists = await _cityRepository.GetByIdAsync(attractionDto.CityId);
            if (cityExists == null)
                throw new KeyNotFoundException($"City with ID {attractionDto.CityId} not found");

            attraction.Name = attractionDto.Name;
            attraction.Description = attractionDto.Description;
            attraction.CityId = attractionDto.CityId;

            await _attractionRepository.UpdateAsync(attraction);
        }

        public async Task DeleteAsync(int id)
        {
            await _attractionRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AttractionDto>> GetAllWithDetailsAsync()
        {
            var attractions = await _attractionRepository.GetAllWithDetailsAsync();
            var attractionDtos = new List<AttractionDto>();

            foreach (var attraction in attractions)
            {
                attractionDtos.Add(new AttractionDto
                {
                    Id = attraction.Id,
                    Name = attraction.Name,
                    Description = attraction.Description,
                    CityId = attraction.CityId,
                    CityName = attraction.City?.Name ?? "Unknown"
                });
            }

            return attractionDtos;
        }
    }
}