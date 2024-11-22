using MediatR;
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
        Task<T> GetByIdAsync(int id);
        Task<Unit> CreateCommandAsync(T entity);
        Task<Unit> UpdateCommandAsync(T entity);
        Task<Unit> DeleteCommandAsync(T entity);
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
        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task<Unit> CreateCommandAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> UpdateCommandAsync(T entity)

        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }

        public async Task<Unit> DeleteCommandAsync(T entity)
        {
            DbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
      
    }
}
