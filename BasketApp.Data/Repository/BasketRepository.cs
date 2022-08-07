using BasketApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketApp.Data.Repository
{
    public class BasketRepository
    {
        protected readonly DbContext _context;
        protected readonly DbSet<Product> _dbSet;

        public BasketRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Product>();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached; // memory den at  
            }

            return entity;
        }
    }
}
