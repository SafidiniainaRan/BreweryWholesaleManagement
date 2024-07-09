using BreweryWholesaleManagement.ModelViews;

namespace BreweryWholesaleManagement.Services
{
    public interface IQuoteService
    {
        decimal GetAmountToBePaied(double discount, decimal totalPrice);
        double GetDiscount(int beerQuantity);
        decimal GetTotalPrice(List<BeerQuoteModelView> Beers);
    }
}
