using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;

namespace BreweryWholesaleManagement.Repositories
{
    public interface IBrewerRepository
    {
        Task AddBeerAsync(int brewerId, Beer beer);
        Task DeleteBeerAsync(int breweryId, int beerId);
        Task<PaginatedList<BeerModelView>> GetBeerByBrewerAsync(int brewerId, int pageIndex, int pageSize);
    }
}
