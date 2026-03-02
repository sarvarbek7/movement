using Microsoft.Extensions.Logging;
using Movement.Application.Data.Repositories;
using Movement.Application.Network.Apis.VirtualOffice;
using Movement.Application.OrchestartionServices.Users;
using Movement.Domain.Users.Entities;

namespace Movement.Infrastructure.OrchestartionServices.Users;

internal class UserOrchestrationService(IUserRepository userRepository, IVirtualOfficeHttpService virtualOfficeHttpService, ILogger<UserOrchestrationService> logger) : IUserOrchestrationService
{
    public async Task<User> GetOrCreateUserByPinflAsync(string pinfl, CancellationToken cancellationToken)
    {
        var storedUser = await userRepository.GetByPinflAsync(pinfl, tracked: false, cancellationToken);

        if (storedUser is null)
        {
            try
            {
                var networkUser = await virtualOfficeHttpService.GetUserByPinfl(pinfl, cancellationToken);

                var user = VirtualOfficeMapper.MapToUser(networkUser);

                await userRepository.BeginTransactionAsync(cancellationToken: cancellationToken);

                await userRepository.AddAsync(user, saveChanges: true, cancellationToken);

                await userRepository.CommitTransactionAsync(cancellationToken);

                return user;
            }
            catch (Exception ex)
            {
                await userRepository.RollbackTransactionAsync(cancellationToken);
                logger.LogError(ex, "Failed to fetch user with pinfl {Pinfl} from virtual office", pinfl);
                throw;
            }

        }

        return storedUser;
    }
}