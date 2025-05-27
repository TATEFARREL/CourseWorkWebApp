using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly ITourApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public VoucherService(
            IVoucherRepository voucherRepository,
            ITourApplicationRepository applicationRepository,
            IMapper mapper)
        {
            _voucherRepository = voucherRepository;
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<VoucherDto> GetByIdAsync(int id)
        {
            var voucher = await _voucherRepository.GetByIdAsync(id);
            return _mapper.Map<VoucherDto>(voucher);
        }

        public async Task<VoucherDto> GetByCodeAsync(string code)
        {
            var voucher = await _voucherRepository.GetByCodeAsync(code);
            return _mapper.Map<VoucherDto>(voucher);
        }

        public async Task<VoucherDto> CreateAsync(int tourApplicationId)
        {
            var application = await _applicationRepository.GetByIdAsync(tourApplicationId);
            if (application == null)
                throw new KeyNotFoundException("Application not found");

            var voucher = new Voucher
            {
                TourApplicationId = tourApplicationId,
                Code = GenerateVoucherCode(),
                IssueDate = DateTime.UtcNow,
                IsUsed = false
            };

            await _voucherRepository.AddAsync(voucher);
            return _mapper.Map<VoucherDto>(voucher);
        }

        public async Task<bool> MarkAsUsedAsync(int id)
        {
            return await _voucherRepository.MarkAsUsedAsync(id);
        }

        private string GenerateVoucherCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
        public async Task<IEnumerable<VoucherDto>> GetUserVouchersAsync(string userId)
        {
            var vouchers = await _voucherRepository.GetUserVouchersAsync(userId);
            var voucherDtos = new List<VoucherDto>();
            var today = DateTime.Today;

            foreach (var voucher in vouchers)
            {
                
                if (!voucher.IsUsed && voucher.TourApplication?.Tour?.StartDate <= today)
                {
                    voucher.IsUsed = true;
                    await _voucherRepository.UpdateAsync(voucher);
                }

                voucherDtos.Add(new VoucherDto
                {
                    Id = voucher.Id,
                    Code = voucher.Code,
                    IssueDate = voucher.IssueDate,
                    IsUsed = voucher.IsUsed,
                    TourApplicationId = voucher.TourApplicationId,
                    TourName = voucher.TourApplication?.Tour?.Name ?? "Unknown Tour",
                    TourStartDate = voucher.TourApplication?.Tour?.StartDate ?? DateTime.MinValue
                });
            }

            return voucherDtos;
        }
    }
}