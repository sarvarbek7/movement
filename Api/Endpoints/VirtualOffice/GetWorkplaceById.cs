using Microsoft.AspNetCore.Http.HttpResults;
using Movement.Application.Network.Apis.VirtualOffice;
using Movement.Application.Network.Apis.VirtualOffice.Models;

namespace Movement.Api.Endpoints.VirtualOffice;

public static class GetWorkplaceById
{
    public const string Name = "GetWorkplaceById";
    public const string Path = "workplace/{id:int}";

    public static RouteHandlerBuilder MapGetWorkplaceByIdEndpoint(this RouteGroupBuilder group)
    {
        return group.MapGet(Path, Handler)
            .WithName(Name);

    }

    private static async Task<Results<Ok<Workplace>, NotFound>> Handler(int id, IVirtualOfficeHttpService virtualOfficeHttpService, CancellationToken cancellationToken)
    {
        var result = await virtualOfficeHttpService.GetWorkplaceById(id, cancellationToken);

        return TypedResults.Ok(result);
    }
}