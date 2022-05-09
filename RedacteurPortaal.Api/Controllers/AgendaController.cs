using Microsoft.AspNetCore.Mvc;

namespace RedacteurPortaal.Api.Controllers;

public class AgendaController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}