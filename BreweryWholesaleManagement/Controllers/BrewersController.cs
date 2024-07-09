using BreweryWholesaleManagement.Helpers;
using BreweryWholesaleManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreweryWholesaleManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrewersController : ControllerBase
    {
        private readonly IBrewerService _brewerService;
       
        public BrewersController(IBrewerService breweryService)
        {
            _brewerService = breweryService;
        }

        [HttpGet("{brewerId}/beers")]
        public async Task<IActionResult> GetBeersByBrewer(int brewerId, int pageIndex = Constantes.Pagination.PageIndex, int pageSize = Constantes.Pagination.PageSize)
        {
            var beers = await _brewerService.GetBeerByBrewerAsync(brewerId, pageIndex, pageSize);
            return Ok(beers);
        }
    }
}
