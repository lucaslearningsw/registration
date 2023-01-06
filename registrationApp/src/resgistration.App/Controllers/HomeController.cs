using Microsoft.AspNetCore.Mvc;

namespace resgistration.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
