using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BusService : IBusService
    {
        private readonly IBusRepository _busRepository;
        private readonly IMapper _mapper;

        public BusService(IBusRepository busRepository, IMapper mapper)
        {
            _busRepository = busRepository;
            _mapper = mapper;
        }

        public async Task<BusDto> GetByIdAsync(int id)
        {
            var bus = await _busRepository.GetByIdAsync(id);
            return _mapper.Map<BusDto>(bus);
        }

        public async Task<IEnumerable<BusDto>> GetAllAsync()
        {
            var buses = await _busRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BusDto>>(buses);
        }

        public async Task<IEnumerable<BusDto>> GetAvailableBusesAsync(DateTime startDate, DateTime endDate)
        {
            var buses = await _busRepository.GetAvailableBusesAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<BusDto>>(buses);
        }

        public async Task<BusDto> CreateAsync(BusDto busDto)
        {
            try
            {
                Console.WriteLine($"Creating bus: {busDto.LicensePlate}");
                var bus = _mapper.Map<Bus>(busDto);
                await _busRepository.AddAsync(bus);
                Console.WriteLine($"Bus created with ID: {bus.Id}");
                return _mapper.Map<BusDto>(bus);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating bus: {ex}");
                throw;
            }
        }

        public async Task UpdateAsync(BusDto busDto)
        {
            var bus = await _busRepository.GetByIdAsync(busDto.Id);
            if (bus == null)
                throw new KeyNotFoundException("Bus not found");

            _mapper.Map(busDto, bus);
            await _busRepository.UpdateAsync(bus);
        }

        public async Task DeleteAsync(int id)
        {
            await _busRepository.DeleteAsync(id);
        }
    }
}