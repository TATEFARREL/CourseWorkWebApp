using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TravelAgency.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTourCompositionController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ITourAttractionService _tourAttractionService;
        private readonly IAttractionService _attractionService;

        public AdminTourCompositionController(
            ITourService tourService,
            ITourAttractionService tourAttractionService,
            IAttractionService attractionService)
        {
            _tourService = tourService;
            _tourAttractionService = tourAttractionService;
            _attractionService = attractionService;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllAsync();
            return View(tours);
        }

        public async Task<IActionResult> TourAttractions(int tourId)
        {
            var tour = await _tourService.GetByIdAsync(tourId);
            if (tour == null)
            {
                return NotFound();
            }

            ViewBag.TourId = tourId;
            ViewBag.TourName = tour.Name;

            var attractions = await _tourAttractionService.GetByTourIdAsync(tourId);
            return View(attractions);
        }

        public async Task<IActionResult> AddAttraction(int tourId)
        {
            ViewBag.TourId = tourId;
            var allAttractions = await _attractionService.GetAllWithDetailsAsync();
            return View(allAttractions);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttractionToTour(int tourId, int attractionId, int visitOrder)
        {
            var result = await _tourAttractionService.CreateAsync(new TourAttractionDto
            {
                TourId = tourId,
                AttractionId = attractionId,
                VisitOrder = visitOrder
            });

            if (result != null)
            {
                TempData["SuccessMessage"] = "Attraction added successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add attraction.";
            }

            return RedirectToAction("TourAttractions", new { tourId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAttractionFromTour(int tourId, int attractionId)
        {
            await _tourAttractionService.DeleteAsync(tourId, attractionId);
            TempData["SuccessMessage"] = "Attraction removed successfully!";
            return RedirectToAction("TourAttractions", new { tourId });
        }
    }
}