﻿using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;
using BreweryWholesaleManagement.Repositories;

namespace BreweryWholesaleManagement.Services
{
    public class BrewerService : IBrewerService
    {
        private readonly IBrewerRepository _breweryRepository;

        public BrewerService(IBrewerRepository breweryRepository)
        {
            _breweryRepository = breweryRepository;
        }

        public async Task<PaginatedList<BeerModelView>> GetBeerByBrewerAsync(int brewerId, int pageIndex, int pageSize)
        {
            return await _breweryRepository.GetBeerByBrewerAsync(brewerId, pageIndex, pageSize);
        }
    }
}
