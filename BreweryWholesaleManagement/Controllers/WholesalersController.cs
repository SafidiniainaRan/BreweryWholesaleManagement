using BreweryWholesaleManagement.Mappers;
using BreweryWholesaleManagement.Models.Requests;
using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.Services;
using Microsoft.AspNetCore.Mvc;
using BreweryWholesaleManagement.Helpers;

namespace BreweryWholesaleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalersController : ControllerBase
    {
        private readonly IWholesalerService _wholesalerService;
        private readonly IWholesalerStockMapper _wholesalerStockMapper;

        public WholesalersController(IWholesalerService wholesalerService, IWholesalerStockMapper wholesarStockMapper)
        {
            _wholesalerService = wholesalerService;
            _wholesalerStockMapper = wholesarStockMapper;
        }

        /// <summary>
        /// Add the sale of an existing beer to an existing wholesaler
        /// </summary>
        [HttpPost("{wholesalerId}/beers")]
        public async Task<IActionResult> AddBeerSale(int wholesalerId, BeerSaleRequest beerSale)
        {
            WholesalerStock wholesalerStock = _wholesalerStockMapper.Map(wholesalerId, beerSale);
            await _wholesalerService.AddBeerSaleAsync(wholesalerStock);
            return Ok();
        }

        /// <summary>
        /// List all Beer By wholesale
        /// </summary>
        [HttpGet("{wholesalerId}/beers")]
        public async Task<IActionResult> GetBeerWholesaler(int wholesalerId, int pageIndex = Constantes.Pagination.PageIndex, int pageSize = Constantes.Pagination.PageSize)
        {
            var beers = await _wholesalerService.GetBeerByWholesalerAsync(wholesalerId, pageIndex, pageSize);
            return Ok(beers);
        }

        /// <summary>
        /// A client can request a quote from a wholesaler
        /// </summary>
        [HttpPost("{wholesalerId}/quotes")]
        public async Task<IActionResult> GetQuote(int wholesalerId, [FromBody] QuoteRequest quoteRequest)
        {
            var quoteResponse = await _wholesalerService.GetQuoteAsync(wholesalerId, quoteRequest);
            return Ok(quoteResponse);
        }

        /// <summary>
        /// wholesaler can update the remaining quantity of a beer in his stock
        /// </summary>
        [HttpPut("{wholesalerId}/beers/{beerId}")]
        public async Task<IActionResult> UpdateBeerStock(int wholesalerId, int beerId, BeerStockRequest stock)
        {
            WholesalerStock wholesalerStock = _wholesalerStockMapper.Map(wholesalerId, beerId, stock);
            await _wholesalerService.UpdateBeerStockAsync(wholesalerStock);
            return Ok();
        }
    }
}
