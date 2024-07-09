using BreweryWholesaleManagement.ModelViews;

namespace BreweryWholesaleManagement.Services
{
    public class QuoteService : IQuoteService
    {
        public decimal GetAmountToBePaied(double discount, decimal totalPrice)
        {
            decimal discountAmount = totalPrice * (decimal)discount;
            return totalPrice - discountAmount;
        }

        public double GetDiscount(int beerQuantity)
        {
            if (beerQuantity > 20)
            {
                return 0.2;
            }
            else if (beerQuantity > 10)
            {
                return 0.1;
            }
            else
            {
                return 0;
            }
        }

        public decimal GetTotalPrice(List<BeerQuoteModelView> Beers)
        {
            return Beers.Sum(b => b.Quantity * b.UnitPrice);
        }
    }
}
