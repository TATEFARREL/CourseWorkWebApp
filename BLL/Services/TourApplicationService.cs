using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BLL.Services
{
    public class TourApplicationService : ITourApplicationService
    {
        private readonly ITourApplicationRepository _applicationRepository;
        private readonly ITourRepository _tourRepository;
        private readonly IMapper _mapper;

        public TourApplicationService(
            ITourApplicationRepository applicationRepository,
            ITourRepository tourRepository,
            IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _tourRepository = tourRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdminTourApplicationDto>> GetAllAsync()
        {
            var applications = await _applicationRepository.GetAllWithDetailsAsync();
            var applicationDtos = new List<AdminTourApplicationDto>();

            foreach (var app in applications)
            {
                applicationDtos.Add(new AdminTourApplicationDto
                {
                    Id = app.Id,
                    UserId = app.UserId,
                    UserFullName = app.User?.FullName ?? "Unknown User",
                    TourId = app.TourId,
                    TourName = app.Tour?.Name ?? "Unknown Tour",
                    RequestDate = app.RequestDate,
                    Status = app.Status,
                    HasVoucher = app.Voucher != null
                });
            }

            return applicationDtos;
        }

        public async Task<TourApplicationDto> GetByIdAsync(int id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            return _mapper.Map<TourApplicationDto>(application);
        }
        public async Task<AdminTourApplicationDto> GetByIdAsyncAdmin(int id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            return _mapper.Map<AdminTourApplicationDto>(application);
        }
        public async Task<IEnumerable<TourApplicationDto>> GetByUserIdAsync(string userId)
        {
            var applications = await _applicationRepository.GetUserApplicationsWithTourAsync(userId);
            var applicationDtos = new List<TourApplicationDto>();

            foreach (var app in applications)
            {
                applicationDtos.Add(new TourApplicationDto
                {
                    Id = app.Id,
                    UserId = app.UserId,
                    TourId = app.TourId,
                    TourName = app.Tour?.Name ?? "Unknown Tour",
                    RequestDate = app.RequestDate,
                    Status = app.Status
                });
            }

            return applicationDtos;
        }

        public async Task<IEnumerable<TourApplicationDto>> GetByTourIdAsync(int tourId)
        {
            var applications = await _applicationRepository.GetByTourIdAsync(tourId);
            return _mapper.Map<IEnumerable<TourApplicationDto>>(applications);
        }

        public async Task<TourApplicationDto> CreateAsync(TourApplicationDto applicationDto)
        {
            
            var tourExists = await _tourRepository.ExistsAsync(applicationDto.TourId);
            if (!tourExists)
                throw new KeyNotFoundException("Tour not found");

            var application = new TourApplication
            {
                UserId = applicationDto.UserId,
                TourId = applicationDto.TourId,
                RequestDate = DateTime.UtcNow,
                Status = "Pending"
            };

            await _applicationRepository.AddAsync(application);

            return new TourApplicationDto
            {
                Id = application.Id,
                UserId = application.UserId,
                TourId = application.TourId,
                RequestDate = application.RequestDate,
                Status = application.Status
            };
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            await _applicationRepository.UpdateStatusAsync(id, status);
        }

        public async Task DeleteAsync(int id)
        {
            await _applicationRepository.DeleteAsync(id);
        }

    }
}