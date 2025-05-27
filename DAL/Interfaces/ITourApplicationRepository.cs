using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITourApplicationRepository : IRepository<TourApplication>
    {
        Task<IEnumerable<TourApplication>> GetByUserIdAsync(string userId);
        Task<IEnumerable<TourApplication>> GetByTourIdAsync(int tourId);
        Task UpdateStatusAsync(int id, string status);
        Task<IEnumerable<TourApplication>> GetAllWithDetailsAsync();
        Task<IEnumerable<TourApplication>> GetUserApplicationsWithTourAsync(string userId);
    }
}