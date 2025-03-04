using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.AppUsers
                .Include(m => m.Photos)
                .Include(m => m.Address)
                .ToListAsync(cancellationToken);
        }

        public async Task<AppUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _context.AppUsers.FindAsync(id, cancellationToken);
        }

        public async Task<AppUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await _context.AppUsers.Include(m => m.Photos).FirstOrDefaultAsync(m => m.UserName == username, cancellationToken);
        }

        public async Task<bool> SaveAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public void Update(AppUser user, CancellationToken cancellationToken = default)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
