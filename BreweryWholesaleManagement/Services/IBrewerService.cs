using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;

namespace BreweryWholesaleManagement.Services
{
    public interface IBrewerService
    {
        Task<PaginatedList<BeerModelView>> GetBeerByBrewerAsync(int brewerId, int pageIndex, int pageSize);
    }
}
