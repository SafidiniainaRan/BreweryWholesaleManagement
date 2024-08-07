﻿using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;

namespace BreweryWholesaleManagement.Repositories
{
    public interface IWholesalerRepository
    {
        Task AddBeerSaleAsync(WholesalerStock wholesalerStock);
        Task<WholeSalerModelView?> FindById(int wholesarId);
        Task<PaginatedList<BeerWholesalerModelView>> GetBeerByWholesalerAsync(int wholesalerId, int pageIndex, int pageSize);
        Task<List<BeerWholesalerModelView>> GetBeerByWholesalerAsync(int wholesalerId, List<int> beerIds);
        Task UpdateBeerStockAsync(WholesalerStock wholesalerStock);
    }
}
