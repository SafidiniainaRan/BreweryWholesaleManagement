using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;
using BreweryWholesaleManagement.Repositories;

namespace BreweryWholesaleManagement.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly IWholesalerRepository _wholesalerRepository;

        public WholesalerService(IWholesalerRepository wholesalerRepository)
        {
            _wholesalerRepository = wholesalerRepository;
        }

        public async Task AddBeerSaleAsync(WholesalerStock wholesalerStock)
        {
            await _wholesalerRepository.AddBeerSaleAsync(wholesalerStock);
        }

        public async Task<PaginatedList<BeerWholesalerModelView>> GetBeerByWholesalerAsync(int wholesalerId, int pageIndex, int pageSize)
        {
            return await _wholesalerRepository.GetBeerByWholesalerAsync(wholesalerId, pageIndex, pageSize);
        }
    }
}
