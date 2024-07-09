using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;

namespace BreweryWholesaleManagement.Repositories
{
    public interface IWholesalerRepository
    {
        Task AddBeerSaleAsync(WholesalerStock wholesalerStock);
        Task<PaginatedList<BeerWholesalerModelView>> GetBeerByWholesalerAsync(int wholesalerId, int pageIndex, int pageSize);
    }
}
