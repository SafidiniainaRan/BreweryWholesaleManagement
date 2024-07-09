using System.ComponentModel.DataAnnotations;

namespace BreweryWholesaleManagement.Models.Requests
{
    public class BeerSaleRequest
    {
        public int BeerId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
