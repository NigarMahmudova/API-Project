using Microsoft.AspNetCore.Mvc;

namespace EducationApp.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
