using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Movement.Application.Network.Apis.VirtualOffice;
using Movement.Application.Network.Apis.VirtualOffice.Models;

namespace Movement.Infrastructure.Network.Apis.VirtualOffice;

internal class VirtualOfficeHttpService(HttpClient httpClient,
ILogger<VirtualOfficeHttpService> logger,
                                        IOptions<VirtualOfficeApiSettings> options) : IVirtualOfficeHttpService
{
    private readonly VirtualOfficeApiSettings _settings = options.Value;
    public async Task<Department> GetDepartmentById(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = string.Format(_settings.Endpoints.GetDepartmentById, id);

            var department = await httpClient.GetFromJsonAsync<Department>(url, cancellationToken);

            if (department is null)
            {
                // TODO: Consider using a more specific exception type or a custom exception class
                logger.LogWarning("Department with ID {DepartmentId} not found in Virtual Office API.", id);
                throw new NotImplementedException();
            }

            return department;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching department with ID {DepartmentId} from Virtual Office API.", id);
            throw;
        }
    }

    public async Task<Department> GetStationByCode(string code, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = string.Format(_settings.Endpoints.GetStationByCode, code);

            var station = await httpClient.GetFromJsonAsync<Department>(url, cancellationToken);

            if (station is null)
            {
                // TODO: Consider using a more specific exception type or a custom exception class
                logger.LogWarning("Station with code {StationCode} not found in Virtual Office API.", code);
                throw new NotImplementedException();
            }

            return station;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching station with code {StationCode} from Virtual Office API.", code);
            throw;
        }
    }

    public async Task<User> GetUserById(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = string.Format(_settings.Endpoints.GetUserById, id);

            var user = await httpClient.GetFromJsonAsync<User>(url, cancellationToken);

            if (user is null)
            {
                // TODO: Consider using a more specific exception type or a custom exception class
                logger.LogWarning("User with ID {UserId} not found in Virtual Office API.", id);
                throw new NotImplementedException();
            }
            return user;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching user with ID {UserId} from Virtual Office API.", id);
            throw;
        }
    }

    public async Task<User> GetUserByPinfl(string pinfl, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = string.Format(_settings.Endpoints.GetUserByPinfl, pinfl);

            var user = await httpClient.GetFromJsonAsync<User>(url, cancellationToken);

            if (user is null)
            {
                // TODO: Consider using a more specific exception type or a custom exception class
                logger.LogWarning("User with PINFL {Pinfl} not found in Virtual Office API.", pinfl);
                throw new NotImplementedException();
            }

            return user;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching user with PINFL {Pinfl} from Virtual Office API.", pinfl);
            throw;
        }
    }

    public async Task<Workplace> GetWorkplaceById(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = string.Format(_settings.Endpoints.GetWorkplaceById, id);

            var workplace = await httpClient.GetFromJsonAsync<Workplace>(url, cancellationToken);

            if (workplace is null)
            {
                // TODO: Consider using a more specific exception type or a custom exception class
                logger.LogWarning("Workplace with ID {WorkplaceId} not found in Virtual Office API.", id);
                throw new NotImplementedException();
            }
            return workplace;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching workplace with ID {WorkplaceId} from Virtual Office API.", id);
            throw;
        }
    }
}