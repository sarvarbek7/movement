using Movement.Application.Network.Apis.VirtualOffice.Models;

namespace Movement.Application.Network.Apis.VirtualOffice;

public interface IVirtualOfficeHttpService
{
    Task<User> GetUserByPinfl(string pinfl, CancellationToken cancellationToken = default);
    Task<User> GetUserById(int id, CancellationToken cancellationToken = default);
    Task<Department> GetDepartmentById(int id, CancellationToken cancellationToken = default);
    Task<Workplace> GetWorkplaceById(int id, CancellationToken cancellationToken = default);
    Task<Department> GetStationByCode(string code, CancellationToken cancellationToken = default);
}