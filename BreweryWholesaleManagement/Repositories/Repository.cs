using BreweryWholesaleManagement.Data;
using BreweryWholesaleManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleManagement.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseObject
    {
        private readonly BreweryContext _context;

        public Repository(BreweryContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                                 .Where(e => e.DeletedAt == null)
                                 .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                                .Where(e => e.Id == id && e.DeletedAt == null)
                                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await this.GetByIdAsync(id);
            if (entity != null)
            {
                entity.DeletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> IsExistAsync(int id)
        {
            return _context.Set<T>().AnyAsync(e => e.Id == id);
        }
    }
}
