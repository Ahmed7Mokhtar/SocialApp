using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        void Update(AppUser user, CancellationToken cancellationToken = default);
        Task<bool> SaveAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<AppUser>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AppUser?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<AppUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    }
}
