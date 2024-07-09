namespace BreweryWholesaleManagement.Models.Requests
{
    public class QuoteRequest
    {
        public List<QuoteItemRequest> Items { get; set; } = new List<QuoteItemRequest>();
    }
}
