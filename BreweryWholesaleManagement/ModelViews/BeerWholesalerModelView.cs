namespace BreweryWholesaleManagement.ModelViews
{
    public class BeerWholesalerModelView
    {
        public int BeerId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int RemainingQuantity { get; set; } 
    }
}
