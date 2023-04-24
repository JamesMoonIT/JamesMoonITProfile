using Microsoft.AspNetCore.Mvc;

namespace JamesMoonPortfolioRedux.Controllers
{
    public class RiveScriptController : Controller
    {
        private readonly string _riveScriptPath = @"\chatbot\brain\bigbrain.rive";

        [HttpGet]
        public IActionResult GetRivescript()
        {
            var fullURL = Url.Content(_riveScriptPath);
            return Content(fullURL, "text/plain");
        }
    }
}