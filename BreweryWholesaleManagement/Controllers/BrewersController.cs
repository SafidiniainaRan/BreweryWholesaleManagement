using BreweryWholesaleManagement.Helpers;
using BreweryWholesaleManagement.Mappers;
using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.Models.Requests;
using BreweryWholesaleManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreweryWholesaleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrewersController : ControllerBase
    {
        private readonly IBrewerService _brewerService;
        private readonly IBeerMapper _beerMapper;

        public BrewersController(IBrewerService breweryService, IBeerMapper beerMapper)
        {
            _brewerService = breweryService;
            _beerMapper = beerMapper;
        }

        /// <summary>
        /// A brewer can add new beer
        /// </summary>
        [HttpPost("{breweryId}/beers")]
        public async Task<IActionResult> AddBeer(int breweryId, [FromBody] AddBeerBrewerRequest beer)
        {
            Beer _beer = _beerMapper.Map(beer);
            await _brewerService.AddBeerAsync(breweryId, _beer);
            return Ok();
        }

        /// <summary>
        /// A brewer can delete a beer.
        /// </summary>
        [HttpDelete("{breweryId}/beers/{beerId}")]
        public async Task<IActionResult> DeleteBeer(int breweryId, int beerId)
        {
            await _brewerService.DeleteBeerAsync(breweryId, beerId);
            return Ok();
        }

        /// <summary>
        /// List all the beers by brewery.
        /// </summary>
        [HttpGet("{brewerId}/beers")]
        public async Task<IActionResult> GetBeersByBrewer(int brewerId, int pageIndex = Constantes.Pagination.PageIndex, int pageSize = Constantes.Pagination.PageSize)
        {
            var beers = await _brewerService.GetBeerByBrewerAsync(brewerId, pageIndex, pageSize);
            return Ok(beers);
        }
    }
}
