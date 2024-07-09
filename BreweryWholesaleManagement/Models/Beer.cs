namespace BreweryWholesaleManagement.Models
{
    public class Beer : BaseObject
    {
        public string Name { get; set; }
        public double AlcoholContent { get; set; }
        public decimal Price { get; set; }
        public int BrewerId { get; set; }

        public Brewer Brewer { get; set; }
        
        public ICollection<WholesalerStock> WholesalerStocks { get; set; }
    }
}
