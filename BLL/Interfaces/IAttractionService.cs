using BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAttractionService
    {
        Task<AttractionDto> GetByIdAsync(int id);
        Task<IEnumerable<AttractionDto>> GetAllAsync();
        Task<IEnumerable<AttractionDto>> GetByCityIdAsync(int cityId);
        Task<AttractionDto> CreateAsync(AttractionDto attractionDto);
        Task UpdateAsync(AttractionDto attractionDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<AttractionDto>> GetAllWithDetailsAsync();
    }
}