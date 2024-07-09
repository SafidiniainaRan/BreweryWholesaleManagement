using BreweryWholesaleManagement.ModelViews;

namespace BreweryWholesaleManagement.Models.Responses
{
    public class QuoteResponse
    {
        public WholeSalerModelView Wholesaler { get; set; }
        public List<BeerQuoteModelView> Beers { get; set; } = new List<BeerQuoteModelView>();
        public double Discount { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;
        public decimal AmountToBePaied { get; set; } = 0;
    }
}
