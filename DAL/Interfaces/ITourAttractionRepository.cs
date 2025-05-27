
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITourAttractionRepository : IRepository<TourAttraction>
    {
        Task<IEnumerable<TourAttraction>> GetByTourIdAsync(int tourId);
        Task<IEnumerable<TourAttraction>> GetByAttractionIdAsync(int attractionId);
        Task<TourAttraction?> GetByTourAndAttractionAsync(int tourId, int attractionId);
        Task DeleteAsync(TourAttraction entity);
    }
}