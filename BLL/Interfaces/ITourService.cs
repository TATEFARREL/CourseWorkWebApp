using BLL.DTOs;

namespace BLL.Interfaces;

public interface ITourService
{
    Task<TourDto> GetByIdAsync(int id);
    Task<IEnumerable<TourDto>> GetAllAsync();
    Task<IEnumerable<TourDto>> GetAvailableToursAsync();
    Task<TourDto> CreateAsync(TourDto tourDto);
    Task UpdateAsync(TourDto tourDto);
    Task DeleteAsync(int id);
    Task<TourDetailsDto> GetTourDetailsAsync(int id);
    Task<bool> AddAttractionToTourAsync(int tourId, int attractionId, int visitOrder);
}