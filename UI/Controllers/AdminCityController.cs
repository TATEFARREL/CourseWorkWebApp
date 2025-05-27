using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Interfaces;
using System.Threading.Tasks;

public class AdminCityController : Controller
{
    private readonly ICityService _cityService;
    private readonly ICountryService _countryService;

    public AdminCityController(ICityService cityService, ICountryService countryService)
    {
        _cityService = cityService;
        _countryService = countryService;
    }

    public async Task<IActionResult> Index()
    {
        var cities = await _cityService.GetAllWithDetailsAsync();
        return View(cities);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Countries = await _countryService.GetAllAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CityDto cityDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _cityService.CreateAsync(cityDto);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
            {
                ModelState.AddModelError("CountryId", ex.Message);
            }
        }

        ViewBag.Countries = await _countryService.GetAllAsync();
        return View(cityDto);
    }


    public async Task<IActionResult> Edit(int id)
    {
        var city = await _cityService.GetByIdAsync(id);
        if (city == null)
        {
            return NotFound();
        }

        ViewBag.Countries = await _countryService.GetAllAsync();
        return View(city);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CityDto cityDto)
    {
        if (id != cityDto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _cityService.UpdateAsync(cityDto);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
            {
                ModelState.AddModelError("CountryId", ex.Message);
            }
        }

        ViewBag.Countries = await _countryService.GetAllAsync();
        return View(cityDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _cityService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}