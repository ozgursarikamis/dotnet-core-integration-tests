using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TennisBookings.Merchandise.Api.Data;
using TennisBookings.Merchandise.Api.Models.Output;
using TennisBookings.Merchandise.Api.Stock;

namespace TennisBookings.Merchandise.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        public IProductDataRepository ProductRepository { get; }
        public IStockCalculator StockCalculator { get; }

        public StockController(
            IProductDataRepository productRepository,
            IStockCalculator stockCalculator
        )
        {
            ProductRepository = productRepository;
            StockCalculator = stockCalculator;
        }

        [HttpGet("total")]
        public async Task<ActionResult<StockTotalOutputModel>> GetStockTotal()
        {
            var products = await ProductRepository.GetProductsAsync();

            var totalStockCount = StockCalculator.CalculateStockTotal(products);

            return Ok(new StockTotalOutputModel {StockItemTotal = totalStockCount });
        } 
    }
}
