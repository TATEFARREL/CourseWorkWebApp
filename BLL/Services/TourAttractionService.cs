using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TourAttractionService : ITourAttractionService
    {
        private readonly ITourAttractionRepository _tourAttractionRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IAttractionRepository _attractionRepository;
        private readonly IMapper _mapper;

        public TourAttractionService(
            ITourAttractionRepository tourAttractionRepository,
            ITourRepository tourRepository,
            IAttractionRepository attractionRepository,
            IMapper mapper)
        {
            _tourAttractionRepository = tourAttractionRepository;
            _tourRepository = tourRepository;
            _attractionRepository = attractionRepository;
            _mapper = mapper;
        }

        public async Task<TourAttractionDto> GetByIdAsync(int tourId, int attractionId)
        {
            var tourAttraction = await _tourAttractionRepository.GetByTourAndAttractionAsync(tourId, attractionId);
            return _mapper.Map<TourAttractionDto>(tourAttraction);
        }

        public async Task<IEnumerable<TourAttractionDto>> GetByTourIdAsync(int tourId)
        {
            var tourAttractions = await _tourAttractionRepository.GetByTourIdAsync(tourId);
            return _mapper.Map<IEnumerable<TourAttractionDto>>(tourAttractions);
        }

        public async Task<IEnumerable<TourAttractionDto>> GetByAttractionIdAsync(int attractionId)
        {
            var tourAttractions = await _tourAttractionRepository.GetByAttractionIdAsync(attractionId);
            return _mapper.Map<IEnumerable<TourAttractionDto>>(tourAttractions);
        }

        public async Task<TourAttractionDto> CreateAsync(TourAttractionDto tourAttractionDto)
        {
            var tour = await _tourRepository.GetByIdAsync(tourAttractionDto.TourId);
            var attraction = await _attractionRepository.GetByIdAsync(tourAttractionDto.AttractionId);

            if (tour == null || attraction == null)
                throw new KeyNotFoundException("Tour or Attraction not found");

            var existing = await _tourAttractionRepository.GetByTourAndAttractionAsync(
                tourAttractionDto.TourId, tourAttractionDto.AttractionId);

            if (existing != null)
                throw new InvalidOperationException("This attraction is already added to the tour");

            var tourAttraction = _mapper.Map<TourAttraction>(tourAttractionDto);
            await _tourAttractionRepository.AddAsync(tourAttraction);
            return _mapper.Map<TourAttractionDto>(tourAttraction);
        }

        public async Task UpdateAsync(TourAttractionDto tourAttractionDto)
        {
            var tourAttraction = await _tourAttractionRepository.GetByTourAndAttractionAsync(
                tourAttractionDto.TourId, tourAttractionDto.AttractionId);

            if (tourAttraction == null)
                throw new KeyNotFoundException("TourAttraction not found");

            _mapper.Map(tourAttractionDto, tourAttraction);
            await _tourAttractionRepository.UpdateAsync(tourAttraction);
        }

        public async Task DeleteAsync(int tourId, int attractionId)
        {
            var tourAttraction = await _tourAttractionRepository.GetByTourAndAttractionAsync(tourId, attractionId);
            if (tourAttraction != null)
            {
                await _tourAttractionRepository.DeleteAsync(tourAttraction);
            }
        }

        public async Task UpdateVisitOrderAsync(int tourId, int attractionId, int newOrder)
        {
            var tourAttraction = await _tourAttractionRepository.GetByTourAndAttractionAsync(tourId, attractionId);
            if (tourAttraction == null)
                throw new KeyNotFoundException("TourAttraction not found");

            tourAttraction.VisitOrder = newOrder;
            await _tourAttractionRepository.UpdateAsync(tourAttraction);
        }
    }
}