namespace BreweryWholesaleManagement.Models
{
    public class Wholesaler : BaseObject
    {
        public string Name { get; set; }
        public ICollection<WholesalerStock> WholesalerStocks { get; set; }
    }
}