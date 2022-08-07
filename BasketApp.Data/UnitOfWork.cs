using BasketApp.Core.UnitOfWork;

namespace BasketApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        //private readonly InMemoryDbContext _context;
        //public UnitOfWork(InMemoryDbContext dbContext)
        //{
        //    _context = dbContext;
        //}
        public void Commit()
        {
            _context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
