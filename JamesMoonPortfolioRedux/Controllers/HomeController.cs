using Microsoft.AspNetCore.Mvc;

namespace JamesMoonPortfolioRedux.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
