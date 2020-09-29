using Microsoft.AspNetCore.Mvc;

namespace TennisBookings.Merchandise.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        [HttpGet("total")]
        public IActionResult GetStockTotal()
        {
            return Ok();
        }
    }
}
