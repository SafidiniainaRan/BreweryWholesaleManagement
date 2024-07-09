using System.ComponentModel.DataAnnotations;

namespace BreweryWholesaleManagement.Models.Requests
{
    public class AddBeerBrewerRequest
    {
        [Required]
        public string Name { get; set; }

        [Range(0, 100)]
        public double AlcoholContent { get; set; }

        [Range(0.01, 10000)]
        public decimal Price { get; set; }
    }
}
