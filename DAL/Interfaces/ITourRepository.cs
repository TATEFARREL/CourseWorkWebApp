using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces;

public interface ITourRepository : IRepository<Tour>
{
    Task<IEnumerable<Tour>> GetToursWithAttractionsAsync();
    Task<Tour?> GetTourWithAttractionsAsync(int id);
    Task<bool> ExistsAsync(int id);
}