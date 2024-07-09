using BreweryWholesaleManagement.Helpers;
using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.Models.Requests;
using BreweryWholesaleManagement.Models.Responses;
using BreweryWholesaleManagement.ModelViews;
using BreweryWholesaleManagement.Repositories;

namespace BreweryWholesaleManagement.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IQuoteService _quoteService;

        public WholesalerService(IWholesalerRepository wholesalerRepository, IQuoteService quoteService)
        {
            _wholesalerRepository = wholesalerRepository;
            _quoteService = quoteService;
        }

        public async Task AddBeerSaleAsync(WholesalerStock wholesalerStock)
        {
            await _wholesalerRepository.AddBeerSaleAsync(wholesalerStock);
        }

        public async Task<PaginatedList<BeerWholesalerModelView>> GetBeerByWholesalerAsync(int wholesalerId, int pageIndex, int pageSize)
        {
            return await _wholesalerRepository.GetBeerByWholesalerAsync(wholesalerId, pageIndex, pageSize);
        }

        public async Task<QuoteResponse> GetQuoteAsync(int wholesalerId, QuoteRequest quoteRequest)
        {
            var wholesar = await _wholesalerRepository.FindById(wholesalerId) ?? throw new Exception(Constantes.ErrorMessages.WholesalerMustExist);
            var orderItems = quoteRequest.Items;
            
            if (!orderItems.Any())
                throw new Exception(Constantes.ErrorMessages.OrderCannotBeEmpty);

            if (orderItems.GroupBy(o => o.BeerId).Any(g => g.Count() > 1))
                throw new Exception(Constantes.ErrorMessages.NoDuplicatesInOrder);

            List<int> beerIds = quoteRequest.Items
                                            .Select(x => x.BeerId)
                                            .ToList();

            List<BeerWholesalerModelView> beerWholesalers = await _wholesalerRepository.GetBeerByWholesalerAsync(wholesalerId, beerIds);

            List<int> idBeerNotExists = beerIds.Except(beerWholesalers.Select(x => x.BeerId))
                                                .ToList();

            if (idBeerNotExists.Any())
                throw new Exception(String.Format(Constantes.ErrorMessages.BeerMustBeSoldByWholesaler, String.Join(", ", idBeerNotExists)));

            List<BeerQuoteModelView> beerQuoteModelViews = beerWholesalers.Select(x =>
            {

                int quantity = quoteRequest.Items.FirstOrDefault(i => i.BeerId == x.BeerId).Quantity;


                if (quantity > x.RemainingQuantity)
                    throw new Exception(String.Format(Constantes.ErrorMessages.QuantityExceedsStock, x.BeerId));

                return new BeerQuoteModelView
                {
                    Id = x.BeerId,
                    Name = x.Name,
                    Quantity = quantity,
                    UnitPrice = x.UnitPrice
                };
            }).ToList();

            int beerQuantity = beerQuoteModelViews.Sum(x => x.Quantity);
            double discount = _quoteService.GetDiscount(beerQuantity);
            decimal totalPrice = _quoteService.GetTotalPrice(beerQuoteModelViews);

            return new QuoteResponse
            {
                Wholesaler =  wholesar,
                AmountToBePaied = _quoteService.GetAmountToBePaied(discount, totalPrice),
                Beers = beerQuoteModelViews,
                Discount = discount,
                TotalPrice = totalPrice,
            };
        }

        public async Task UpdateBeerStockAsync(WholesalerStock wholesalerStock)
        {
            await _wholesalerRepository.UpdateBeerStockAsync(wholesalerStock);
        }
    }
}
