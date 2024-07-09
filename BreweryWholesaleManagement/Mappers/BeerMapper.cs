using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.Models.Requests;

namespace BreweryWholesaleManagement.Mappers
{
    public class BeerMapper : IBeerMapper
    {
        public Beer Map(AddBeerBrewerRequest addBeerBrewerRequest)
        {
            return new Beer
            {
                AlcoholContent = addBeerBrewerRequest.AlcoholContent,
                Price = addBeerBrewerRequest.Price,
                Name = addBeerBrewerRequest.Name,
                CreatedAt = DateTime.Now,
                DeletedAt = null
            };
        }
    }
}
