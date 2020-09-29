using Microsoft.AspNetCore.Mvc;

namespace TennisBookings.Merchandise.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        [HttpGet("total")]
        public IActionResult GetStockTotal()
        {
            //return Ok("{\"stockItemTotal:\":100}"); // Generates text/plain, not application/json
            return new JsonResult("{\"stockItemTotal:\":100}");
        } 
    }
}
