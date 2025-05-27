using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Interfaces;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IAttractionRepository _attractionRepository;
        private readonly IBusRepository _busRepository;
        private readonly ITourAttractionRepository _tourAttractionRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public TourService(
            ITourRepository tourRepository,
            IAttractionRepository attractionRepository,
            IBusRepository busRepository,
            ITourAttractionRepository tourAttractionRepository,
            ICityRepository cityRepository,
            ICountryRepository countryRepository,
            IMapper mapper)
        {
            _tourRepository = tourRepository;
            _attractionRepository = attractionRepository;
            _busRepository = busRepository;
            _tourAttractionRepository = tourAttractionRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<TourDto> GetByIdAsync(int id)
        {
            var tour = await _tourRepository.GetTourWithAttractionsAsync(id);
            return _mapper.Map<TourDto>(tour);
        }

        public async Task<IEnumerable<TourDto>> GetAllAsync()
        {
            var tours = await _tourRepository.GetToursWithAttractionsAsync();
            return _mapper.Map<IEnumerable<TourDto>>(tours);
        }

        public async Task<IEnumerable<TourDto>> GetAvailableToursAsync()
        {
            var tours = await _tourRepository.GetToursWithAttractionsAsync();
            return _mapper.Map<IEnumerable<TourDto>>(tours.Where(t => t.StartDate > DateTime.Now));
        }

        public async Task<TourDto> CreateAsync(TourDto tourDto)
        {
            var bus = await _busRepository.GetByIdAsync(tourDto.BusId);
            if (bus == null)
                throw new ArgumentException("Bus not found");

            var tour = _mapper.Map<Tour>(tourDto);
            await _tourRepository.AddAsync(tour);
            return _mapper.Map<TourDto>(tour);
        }

        public async Task UpdateAsync(TourDto tourDto)
        {
            var tour = await _tourRepository.GetByIdAsync(tourDto.Id);
            if (tour == null)
                throw new ArgumentException("Tour not found");

            _mapper.Map(tourDto, tour);
            await _tourRepository.UpdateAsync(tour);
        }

        public async Task DeleteAsync(int id)
        {
            await _tourRepository.DeleteAsync(id);
        }

        public async Task<bool> AddAttractionToTourAsync(int tourId, int attractionId, int visitOrder)
        {
            var tour = await _tourRepository.GetByIdAsync(tourId);
            var attraction = await _attractionRepository.GetByIdAsync(attractionId);

            if (tour == null || attraction == null)
                return false;

            var tourAttraction = new TourAttraction
            {
                TourId = tourId,
                AttractionId = attractionId,
                VisitOrder = visitOrder
            };

            await _tourAttractionRepository.AddAsync(tourAttraction);
            return true;
        }

        public async Task<TourDetailsDto> GetTourDetailsAsync(int id)
        {
            var tour = await _tourRepository.GetTourWithAttractionsAsync(id);
            if (tour == null) return null;

            var bus = await _busRepository.GetByIdAsync(tour.BusId);
            var tourAttractions = await _tourAttractionRepository.GetByTourIdAsync(id);

            var detailsDto = new TourDetailsDto
            {
                Id = tour.Id,
                Name = tour.Name,
                Description = tour.Description,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                Price = tour.Price,
                ImageUrl = tour.ImageUrl,
                Bus = bus != null ? _mapper.Map<BusDto>(bus) : null
            };

            foreach (var ta in tourAttractions)
            {
                var attraction = await _attractionRepository.GetByIdAsync(ta.AttractionId);
                if (attraction != null)
                {
                    var city = await _cityRepository.GetByIdAsync(attraction.CityId);
                    var country = city != null ? await _countryRepository.GetByIdAsync(city.CountryId) : null;

                    detailsDto.Attractions.Add(new AttractionDetailsDto
                    {
                        Id = attraction.Id,
                        Name = attraction.Name,
                        Description = attraction.Description,
                        CityId = attraction.CityId,
                        CityName = city?.Name ?? "Unknown",
                        City = city != null ? _mapper.Map<CityDto>(city) : null,
                        Country = country != null ? _mapper.Map<CountryDto>(country) : null,
                        VisitOrder = ta.VisitOrder
                    });
                }
            }

            return detailsDto;
        }
    }
}