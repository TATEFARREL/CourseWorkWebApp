using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Interfaces;
using System.Threading.Tasks;

public class AdminTourController : Controller
{
    private readonly ITourService _tourService;
    private readonly IBusService _busService;

    public AdminTourController(ITourService tourService, IBusService busService)
    {
        _tourService = tourService;
        _busService = busService;
    }

    public async Task<IActionResult> Index()
    {
        var tours = await _tourService.GetAllAsync();
        return View(tours);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Buses = await _busService.GetAllAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TourDto tourDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _tourService.CreateAsync(tourDto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("BusId", ex.Message);
            }
        }

        ViewBag.Buses = await _busService.GetAllAsync();
        return View(tourDto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var tour = await _tourService.GetByIdAsync(id);
        if (tour == null)
        {
            return NotFound();
        }

        ViewBag.Buses = await _busService.GetAllAsync();
        return View(tour);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TourDto tourDto)
    {
        if (id != tourDto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _tourService.UpdateAsync(tourDto);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("BusId", ex.Message);
            }
        }

        ViewBag.Buses = await _busService.GetAllAsync();
        return View(tourDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _tourService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}