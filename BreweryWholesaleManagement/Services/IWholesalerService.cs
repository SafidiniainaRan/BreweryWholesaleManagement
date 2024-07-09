using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.Models.Requests;
using BreweryWholesaleManagement.Models.Responses;
using BreweryWholesaleManagement.ModelViews;

namespace BreweryWholesaleManagement.Services
{
    public interface IWholesalerService
    {
        Task AddBeerSaleAsync(WholesalerStock wholesalerStock);
        Task<PaginatedList<BeerWholesalerModelView>> GetBeerByWholesalerAsync(int wholesalerId, int pageIndex, int pageSize);
        Task<QuoteResponse> GetQuoteAsync(int wholesalerId, QuoteRequest quoteRequest);
        Task UpdateBeerStockAsync(WholesalerStock wholesalerStock);
    }
}
