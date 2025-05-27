using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces;

public interface IAttractionRepository : IRepository<Attraction>
{
    Task<IEnumerable<Attraction>> GetByCityIdAsync(int cityId);
    Task<IEnumerable<Attraction>> GetAllWithDetailsAsync();
    Task<Attraction?> GetByIdWithDetailsAsync(int id);
}