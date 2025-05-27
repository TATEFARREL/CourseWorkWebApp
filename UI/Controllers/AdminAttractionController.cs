using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Interfaces;
using System.Threading.Tasks;

public class AdminAttractionController : Controller
{
    private readonly IAttractionService _attractionService;
    private readonly ICityService _cityService;

    public AdminAttractionController(IAttractionService attractionService, ICityService cityService)
    {
        _attractionService = attractionService;
        _cityService = cityService;
    }

    public async Task<IActionResult> Index()
    {
        var attractions = await _attractionService.GetAllWithDetailsAsync();
        return View(attractions);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Cities = await _cityService.GetAllWithDetailsAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AttractionDto attractionDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _attractionService.CreateAsync(attractionDto);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
            {
                ModelState.AddModelError("CityId", ex.Message);
            }
        }

        ViewBag.Cities = await _cityService.GetAllWithDetailsAsync();
        return View(attractionDto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var attraction = await _attractionService.GetByIdAsync(id);
        if (attraction == null)
        {
            return NotFound();
        }

        ViewBag.Cities = await _cityService.GetAllWithDetailsAsync();
        return View(attraction);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AttractionDto attractionDto)
    {
        if (id != attractionDto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _attractionService.UpdateAsync(attractionDto);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
            {
                ModelState.AddModelError("CityId", ex.Message);
            }
        }

        ViewBag.Cities = await _cityService.GetAllWithDetailsAsync();
        return View(attractionDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _attractionService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}