using Microsoft.AspNetCore.Mvc;
using JamesMoonPortfolioRedux.Components;
using JamesMoonPortfolioRedux.Models;

namespace JamesMoonPortfolioRedux.Controllers
{
    [Route("Tools")]
    public class ToolsController : Controller
    {
        [HttpGet("TimeDifference")]
        public IActionResult TimeDifference()
        {
            return View();
        }

        [HttpPost("GetTimeDifference")]
        public IActionResult GetTimeDifference([FromBody] TimeDifference request)
        {
            if (request.StartDate > request.EndDate)
            {
                (request.StartDate, request.EndDate) = (request.EndDate, request.StartDate);
            }

            try
            {
                // Fix: Use the correct method or logic to calculate the time difference
                var result = TimeDifferenceCalc.CalculateTimeDifference(request.StartDate, request.EndDate);
                return Ok(new { success = true, difference = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
