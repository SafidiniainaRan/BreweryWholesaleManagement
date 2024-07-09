﻿using BreweryWholesaleManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesaleManagement.Services
{
    public class PaginationService<T>
    {
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
