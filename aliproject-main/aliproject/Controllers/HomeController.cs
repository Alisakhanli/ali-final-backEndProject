using Microsoft.AspNetCore.Mvc;

namespace AliProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
