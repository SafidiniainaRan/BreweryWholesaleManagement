using BreweryWholesaleManagement.Data;
using BreweryWholesaleManagement.Models;
using BreweryWholesaleManagement.ModelViews;
using BreweryWholesaleManagement.Services;

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
