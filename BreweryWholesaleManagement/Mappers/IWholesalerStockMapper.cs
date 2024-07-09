﻿using BreweryWholesaleManagement.Models.Requests;
using BreweryWholesaleManagement.Models;

namespace BreweryWholesaleManagement.Mappers
{
    public interface IWholesalerStockMapper
    {
        WholesalerStock Map(int wholesalerId, BeerSaleRequest beerSale);
    }
}
