using Movement.Domain.Users.Entities;

namespace Movement.Application.Data.Repositories;

public interface IUserRepository : IRepository<User, int>
{
    Task<User?> GetByPinflAsync(string pinfl, bool tracked = true, CancellationToken cancellationToken = default);
}