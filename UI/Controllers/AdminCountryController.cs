using Microsoft.AspNetCore.Mvc;
using BLL.DTOs;
using BLL.Interfaces;

public class AdminCountryController : Controller
{
    private readonly ICountryService _countryService;

    public AdminCountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    public async Task<IActionResult> Index()
    {
        var countries = await _countryService.GetAllAsync();
        return View(countries);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CountryDto countryDto)
    {
        if (ModelState.IsValid)
        {
            await _countryService.CreateAsync(countryDto);
            return RedirectToAction(nameof(Index));
        }
        return View(countryDto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var country = await _countryService.GetByIdAsync(id);
        if (country == null)
        {
            return NotFound();
        }
        return View(country);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CountryDto countryDto)
    {
        if (id != countryDto.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _countryService.UpdateAsync(countryDto);
            return RedirectToAction(nameof(Index));
        }
        return View(countryDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _countryService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}