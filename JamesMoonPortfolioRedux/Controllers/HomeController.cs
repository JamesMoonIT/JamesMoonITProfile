using Microsoft.AspNetCore.Mvc;

namespace JamesMoonPortfolioRedux.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _riveScriptPath = @"\chatbot\brain\bigbrain.rive";

        public IActionResult Index()
        {
            return View();
        }
    }
}
