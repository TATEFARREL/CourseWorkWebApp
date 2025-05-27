using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class AdminTourApplicationController : Controller
{
    private readonly ITourApplicationService _applicationService;
    private readonly IVoucherService _voucherService;

    public AdminTourApplicationController(
        ITourApplicationService applicationService,
        IVoucherService voucherService)
    {
        _applicationService = applicationService;
        _voucherService = voucherService;
    }

    public async Task<IActionResult> Index()
    {
        var applications = await _applicationService.GetAllAsync();
        return View(applications);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveApplication(int id)
    {
        await _applicationService.UpdateStatusAsync(id, "Approved");
        TempData["SuccessMessage"] = "Application approved successfully";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> CreateVoucher(int applicationId)
    {
        try
        {
            var voucher = await _voucherService.CreateAsync(applicationId);
            await _applicationService.UpdateStatusAsync(applicationId, "Voucher Issued");
            TempData["SuccessMessage"] = $"Voucher created successfully. Code: {voucher.Code}";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error creating voucher: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteApplication(int id)
    {
        try
        {
            var application = await _applicationService.GetByIdAsyncAdmin(id);
            if (application == null)
            {
                return NotFound();
            }

            if (application.HasVoucher)
            {
                
            }

            await _applicationService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Application deleted successfully";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error deleting application: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }
}