using BreweryWholesaleManagement.Models.Requests;
using BreweryWholesaleManagement.Models;

namespace BreweryWholesaleManagement.Mappers
{
    public interface IBeerMapper
    {
        Beer Map(AddBeerBrewerRequest addBeerBrewerRequest);
    }
}
