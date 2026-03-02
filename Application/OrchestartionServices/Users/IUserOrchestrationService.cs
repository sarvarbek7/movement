using Movement.Domain.Users.Entities;

namespace Movement.Application.OrchestartionServices.Users;

public interface IUserOrchestrationService
{
    Task<User> GetOrCreateUserByPinflAsync(string pinfl, CancellationToken cancellationToken);
}