using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_03.Models;

namespace Test_03.Controllers;

public class HomeController : Controller
{
    private readonly CustomerDBContext dBContext;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, CustomerDBContext dBContext)
    {
        _logger = logger;
        this.dBContext = dBContext;
    }

    public async Task< IActionResult> Index()
    {
        var customer = await dBContext.Customers.Include(s => s.DeliveryDetails).ToListAsync();
        return View(customer);
    }
    [HttpPost]
    public async Task<IActionResult> Save(Customer customer, IFormFile? photo)
    {
        using var trx = await dBContext.Database.BeginTransactionAsync();
        try
        {
            if(photo != null && photo.Length > 0)
            {
                var uploadFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");

                var fileName = Path.GetFileNameWithoutExtension(photo.FileName);
                var ext = Path.GetFileName(photo.FileName);
                var uniqueFileName = $"{fileName}__Sarah__{ext}";

                var filePath = Path.Combine(uploadFile, uniqueFileName);

                using var stream = new FileStream(filePath, FileMode.Create);

                await photo.CopyToAsync(stream);
                customer.Photo = uniqueFileName;
               
            }
            if (customer.CustomerId > 0)
            {

            }
            else
            {
                dBContext.Customers.Add(customer);
            }
            await dBContext.SaveChangesAsync();
            await trx.CommitAsync();
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            await trx.RollbackAsync();
            return BadRequest(ex.Message);
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
