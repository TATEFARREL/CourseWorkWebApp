using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITourApplicationService
    {
        Task<TourApplicationDto> GetByIdAsync(int id);
        Task<AdminTourApplicationDto> GetByIdAsyncAdmin(int id);
        Task<IEnumerable<AdminTourApplicationDto>> GetAllAsync(); 
        Task<IEnumerable<TourApplicationDto>> GetByUserIdAsync(string userId);
        Task<IEnumerable<TourApplicationDto>> GetByTourIdAsync(int tourId);
        Task<TourApplicationDto> CreateAsync(TourApplicationDto applicationDto);
        Task UpdateStatusAsync(int id, string status);
        Task DeleteAsync(int id);

    }
}