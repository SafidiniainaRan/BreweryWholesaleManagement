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

        [HttpPost("{wholesalerId}/beers")]
        public async Task<IActionResult> AddBeerSale(int wholesalerId, BeerSaleRequest beerSale)
        {
            WholesalerStock wholesalerStock = _wholesalerStockMapper.Map(wholesalerId, beerSale);
            await _wholesalerService.AddBeerSaleAsync(wholesalerStock);
            return Ok();
        }

        [HttpGet("{wholesalerId}/beers")]
        public async Task<IActionResult> GetBeerWholesaler(int wholesalerId, int pageIndex = Constantes.Pagination.PageIndex, int pageSize = Constantes.Pagination.PageSize)
        {
            var beers = await _wholesalerService.GetBeerByWholesalerAsync(wholesalerId, pageIndex, pageSize);
            return Ok(beers);
        }
    }
}
