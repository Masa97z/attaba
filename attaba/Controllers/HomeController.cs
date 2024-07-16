using Microsoft.AspNetCore.Mvc;

namespace attaba.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
