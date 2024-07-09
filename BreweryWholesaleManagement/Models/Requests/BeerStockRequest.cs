using System.ComponentModel.DataAnnotations;

namespace BreweryWholesaleManagement.Models.Requests
{
    public class BeerStockRequest
    {
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
