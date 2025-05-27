using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.DTOs;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBusController : Controller
    {
        private readonly IBusService _busService;

        public AdminBusController(IBusService busService)
        {
            _busService = busService;
        }

        public async Task<IActionResult> Index()
        {
            var buses = await _busService.GetAllAsync();
            return View(buses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BusDto busDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _busService.CreateAsync(busDto);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error creating bus: {ex.Message}");
                }
            }

            return View(busDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var bus = await _busService.GetByIdAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BusDto busDto)
        {
            if (id != busDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _busService.UpdateAsync(busDto);
                return RedirectToAction(nameof(Index));
            }
            return View(busDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _busService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}