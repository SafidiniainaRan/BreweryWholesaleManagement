﻿using BreweryWholesaleManagement.Models;

namespace BreweryWholesaleManagement.Repositories
{
    public interface IRepository<T> where T : BaseObject
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> IsExistAsync(int id);
    }
}
