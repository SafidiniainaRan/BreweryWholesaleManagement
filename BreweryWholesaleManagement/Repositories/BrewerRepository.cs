using BreweryWholesaleManagement.Data;
using BreweryWholesaleManagement.Helpers;
using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;
using BreweryWholesaleManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleManagement.Repositories
{
    public class BrewerRepository : IBrewerRepository
    {
        private readonly BreweryContext _context;
        private readonly IRepository<Brewer> _repository;

        public BrewerRepository(BreweryContext context, IRepository<Brewer> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task AddBeerAsync(int brewerId, Beer beer)
        {
            var brewer = await _repository.GetByIdAsync(brewerId);
            if (brewer != null)
            {
                brewer.Beers = new List<Beer>() { beer };
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception(String.Format(Constantes.ErrorMessages.ElementWithIdNodFound, nameof(Brewer), brewerId));
            }
        }

        public async Task DeleteBeerAsync(int breweryId, int beerId)
        {
            var beer = await _context.Beers
                                     .Where(b => b.Id == beerId && b.BrewerId == breweryId)
                                     .FirstOrDefaultAsync();
            if (beer != null)
            {
                if (beer.DeletedAt != null) throw new Exception(String.Format(Constantes.ErrorMessages.ElementWithIdAlreadyDeleted, nameof(Beer), beerId));
                beer.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception(Constantes.ErrorMessages.ElementNotFound);
            }
        }

        public async Task<PaginatedList<BeerModelView>> GetBeerByBrewerAsync(int brewerId, int pageIndex, int pageSize)
        {
            IQueryable<BeerModelView> source = _context.Beers
                                                        .Where(b => b.BrewerId == brewerId && b.DeletedAt == null)
                                                        .Select(beer => new BeerModelView
                                                        {
                                                            Id = beer.Id,
                                                            Price = beer.Price,
                                                            AlcoholContent = beer.AlcoholContent,
                                                            Name = beer.Name
                                                        });
            return await PaginationService<BeerModelView>.CreateAsync(source, pageIndex, pageSize);
        }
    }
}
