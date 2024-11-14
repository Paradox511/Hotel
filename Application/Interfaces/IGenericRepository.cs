using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        DbSet<T> DbSet { get; }
        Task<IEnumerable<T>> GetAllAsync();
        // Other generic methods as needed
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public DbSet<T> DbSet => _context.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }
    }
}
