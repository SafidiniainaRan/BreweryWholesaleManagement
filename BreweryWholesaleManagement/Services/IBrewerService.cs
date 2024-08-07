﻿using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;

namespace BreweryWholesaleManagement.Services
{
    public interface IBrewerService
    {
        Task AddBeerAsync(int brewerId, Beer beer);
        Task DeleteBeerAsync(int breweryId, int beerId);
        Task<PaginatedList<BeerModelView>> GetBeerByBrewerAsync(int brewerId, int pageIndex, int pageSize);
    }
}
