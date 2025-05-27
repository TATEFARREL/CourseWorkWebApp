using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITourAttractionService
    {
        Task<TourAttractionDto> GetByIdAsync(int tourId, int attractionId);
        Task<IEnumerable<TourAttractionDto>> GetByTourIdAsync(int tourId);
        Task<IEnumerable<TourAttractionDto>> GetByAttractionIdAsync(int attractionId);
        Task<TourAttractionDto> CreateAsync(TourAttractionDto tourAttractionDto);
        Task UpdateAsync(TourAttractionDto tourAttractionDto);
        Task DeleteAsync(int tourId, int attractionId);
        Task UpdateVisitOrderAsync(int tourId, int attractionId, int newOrder);
    }
}