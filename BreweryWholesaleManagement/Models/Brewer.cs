namespace BreweryWholesaleManagement.Models
{
    public class Brewer : BaseObject
    {
        public string Name { get; set; }
        public ICollection<Beer> Beers { get; set; }
    }
}