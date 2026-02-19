using Movement.Application.Network.Apis.VirtualOffice;
using Movement.Application.Network.Apis.VirtualOffice.Models;

namespace Movement.Infrastructure.Network.Apis.VirtualOffice;

internal class VirtualOfficeHttpService(HttpClient httpClient) : IVirtualOfficeHttpService
{
    public Task<Department> GetDepartmentById(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Department> GetStationByCode(string code, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByPinfl(string pinfl, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Workplace> GetWorkplaceById(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    // Implement methods to interact with the Virtual Office API
}