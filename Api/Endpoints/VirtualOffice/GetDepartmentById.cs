using Microsoft.AspNetCore.Http.HttpResults;
using Movement.Application.Network.Apis.VirtualOffice;
using Movement.Application.Network.Apis.VirtualOffice.Models;

namespace Movement.Api.Endpoints.VirtualOffice;

public static class GetDepartmentById
{
    public const string Name = "GetDepartmentById";
    public const string Path = "department/{id:int}";

    public static RouteHandlerBuilder MapGetDepartmentByIdEndpoint(this RouteGroupBuilder group)
    {
        return group.MapGet(Path, Handler)
            .WithName(Name);

    }

    private static async Task<Results<Ok<Department>, NotFound>> Handler(int id, IVirtualOfficeHttpService virtualOfficeHttpService, CancellationToken cancellationToken)
    {
        var result = await virtualOfficeHttpService.GetDepartmentById(id, cancellationToken);

        return TypedResults.Ok(result);
    }
}