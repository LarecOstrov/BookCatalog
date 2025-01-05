using BookCatalog.Shared.DbModels;
using BookCatalogBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{  

    public HomeController()
    {
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Api started");
    }    
}