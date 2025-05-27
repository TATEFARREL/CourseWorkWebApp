using BLL.DTOs;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IVoucherService
    {
        Task<VoucherDto> GetByIdAsync(int id);
        Task<VoucherDto> GetByCodeAsync(string code);
        Task<VoucherDto> CreateAsync(int tourApplicationId);
        Task<bool> MarkAsUsedAsync(int id);
        Task<IEnumerable<VoucherDto>> GetUserVouchersAsync(string userId);
    }
}