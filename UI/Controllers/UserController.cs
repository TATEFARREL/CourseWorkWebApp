using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TravelAgency.Controllers
{
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ITourApplicationService _applicationService;
        private readonly IVoucherService _voucherService;

        public UserController(
    ITourService tourService,
    ITourApplicationService applicationService,
    IVoucherService voucherService)
        {
            _tourService = tourService;
            _applicationService = applicationService;
            _voucherService = voucherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AvailableTours()
        {
            var tours = await _tourService.GetAvailableToursAsync();
            return View(tours);
        }

        public async Task<IActionResult> TourDetails(int id)
        {
            var tourDetails = await _tourService.GetTourDetailsAsync(id);
            if (tourDetails == null)
            {
                return NotFound();
            }
            return View(tourDetails);
        }
        public async Task<IActionResult> MyVouchers()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var vouchers = await _voucherService.GetUserVouchersAsync(userId);
                return View(vouchers);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading vouchers: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateApplication(int tourId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var applicationDto = new TourApplicationDto
                {
                    UserId = userId,
                    TourId = tourId
                    
                };

                await _applicationService.CreateAsync(applicationDto);

                TempData["SuccessMessage"] = "Application submitted successfully!";
                return RedirectToAction("AvailableTours");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error creating application: {ex.Message}";
                return RedirectToAction("TourDetails", new { id = tourId });
            }
        }
        public async Task<IActionResult> MyApplications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var applications = await _applicationService.GetByUserIdAsync(userId);
            return View(applications);
        }

        [HttpPost]
        public async Task<IActionResult> CancelApplication(int id)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var application = await _applicationService.GetByIdAsync(id);
                if (application == null || application.UserId != userId)
                {
                    return NotFound();
                }

                await _applicationService.DeleteAsync(id);
                TempData["SuccessMessage"] = "Application canceled successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error canceling application: {ex.Message}";
            }

            return RedirectToAction(nameof(MyApplications));
        }
    }
}