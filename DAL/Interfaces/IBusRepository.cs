using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBusRepository : IRepository<Bus>
    {
        Task<IEnumerable<Bus>> GetAvailableBusesAsync(DateTime startDate, DateTime endDate);
    }
}