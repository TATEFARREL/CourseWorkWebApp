using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher?> GetByCodeAsync(string code);
        Task<bool> MarkAsUsedAsync(int id);
        Task<IEnumerable<Voucher>> GetUserVouchersAsync(string userId);
        Task UpdateAsync(Voucher voucher);
    }
}