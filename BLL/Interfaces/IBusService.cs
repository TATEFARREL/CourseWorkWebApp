using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBusService
    {
        Task<BusDto> GetByIdAsync(int id);
        Task<IEnumerable<BusDto>> GetAllAsync();
        Task<IEnumerable<BusDto>> GetAvailableBusesAsync(DateTime startDate, DateTime endDate);
        Task<BusDto> CreateAsync(BusDto busDto);
        Task UpdateAsync(BusDto busDto);
        Task DeleteAsync(int id);
    }
}