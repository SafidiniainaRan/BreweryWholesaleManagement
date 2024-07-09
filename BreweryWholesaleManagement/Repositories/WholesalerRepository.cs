using BreweryWholesaleManagement.Data;
using BreweryWholesaleManagement.Helpers;
using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;
using BreweryWholesaleManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleManagement.Repositories
{
    public class WholesalerRepository : IWholesalerRepository
    {
        private readonly BreweryContext _context;

        private readonly IRepository<WholesalerStock> _stockRepository;
        private readonly IRepository<Wholesaler> _wholesalerRepository;
        private readonly IRepository<Beer> _beerRepository;
        public WholesalerRepository(BreweryContext context, 
                                    IRepository<WholesalerStock> stockRepository, 
                                    IRepository<Wholesaler> wholesalerRepository,
                                    IRepository<Beer> beerRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
            _beerRepository = beerRepository;
            _wholesalerRepository = wholesalerRepository;
        }

        public async Task AddBeerSaleAsync(WholesalerStock wholesalerStock)
        {
            if (! (await _beerRepository.IsExistAsync(wholesalerStock.BeerId)))
                throw new Exception(String.Format(Constantes.ErrorMessages.ElementWithIdMustExist, nameof(Beer), wholesalerStock.BeerId));

            if (! (await _wholesalerRepository.IsExistAsync(wholesalerStock.WholesalerId)))
                throw new Exception(String.Format(Constantes.ErrorMessages.ElementWithIdMustExist, nameof(Wholesaler), wholesalerStock.WholesalerId));

            var stock = _context.WholesalerStocks
                                .Where(w => w.WholesalerId == wholesalerStock.WholesalerId &&
                                        w.BeerId == wholesalerStock.BeerId &&
                                        w.DeletedAt == null)
                                .FirstOrDefault();
            if (stock == null)
            {
                _context.WholesalerStocks.Add(wholesalerStock);
            }
            else
            {
                stock.Quantity += wholesalerStock.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<WholeSalerModelView?> FindById(int wholesarId)
        {
            return await _context.Wholesalers
                                 .Where(x => x.Id == wholesarId && x.DeletedAt == null)
                                 .Select(x => new WholeSalerModelView
                                 {
                                     Id = x.Id,
                                     Name = x.Name
                                 })
                                 .FirstOrDefaultAsync();
        }

        public async Task<PaginatedList<BeerWholesalerModelView>> GetBeerByWholesalerAsync(int wholesalerId, int pageIndex, int pageSize)
        {
            var source = from beer in _context.Beers
                         join ws in _context.WholesalerStocks on beer.Id equals ws.BeerId
                         where ws.WholesalerId == wholesalerId && ws.DeletedAt == null
                         select new BeerWholesalerModelView
                         {
                             BeerId = beer.Id,
                             Name = beer.Name,
                             RemainingQuantity = ws.Quantity,
                             UnitPrice = beer.Price
                         };
            return await PaginationService<BeerWholesalerModelView>.CreateAsync(source, pageIndex, pageSize);
        }

        public async Task<List<BeerWholesalerModelView>> GetBeerByWholesalerAsync(int wholesalerId, List<int> beerIds)
        {
            var source = from beer in _context.Beers
                         join ws in _context.WholesalerStocks on beer.Id equals ws.BeerId
                         where ws.WholesalerId == wholesalerId && beerIds.Contains(ws.BeerId) && ws.DeletedAt == null
                         select new BeerWholesalerModelView
                         {
                             BeerId = beer.Id,
                             Name = beer.Name,
                             RemainingQuantity = ws.Quantity,
                             UnitPrice = beer.Price
                         };
            return await source.ToListAsync();
        }

        public async Task UpdateBeerStockAsync(WholesalerStock wholesalerStock)
        {
            if (!(await _beerRepository.IsExistAsync(wholesalerStock.BeerId)))
                throw new Exception(String.Format(Constantes.ErrorMessages.ElementWithIdMustExist, nameof(Beer), wholesalerStock.BeerId));

            if (!(await _wholesalerRepository.IsExistAsync(wholesalerStock.WholesalerId)))
                throw new Exception(String.Format(Constantes.ErrorMessages.ElementWithIdMustExist, nameof(Wholesaler), wholesalerStock.WholesalerId));

            var stock = _context.WholesalerStocks
                                .Where(w => w.WholesalerId == wholesalerStock.WholesalerId &&
                                        w.BeerId == wholesalerStock.BeerId &&
                                        w.DeletedAt == null)
                                .FirstOrDefault();
            if (stock != null)
            {
                stock.Quantity = wholesalerStock.Quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception(String.Format(Constantes.ErrorMessages.BeerMustBeSoldByWholesaler, wholesalerStock.BeerId));
            }
        }
    }
}
