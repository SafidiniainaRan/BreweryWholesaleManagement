using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.Models.Requests;

namespace BreweryWholesaleManagement.Mappers
{
    public class WholesalerStockMapper : IWholesalerStockMapper
    {
        public WholesalerStock Map(int wholesalerId, BeerSaleRequest beerSale)
        {
            return new WholesalerStock
            {
                WholesalerId = wholesalerId,
                BeerId = beerSale.BeerId,
                Quantity = beerSale.Quantity,
                CreatedAt = DateTime.Now,
                DeletedAt = null
            };
        }
    }
}
