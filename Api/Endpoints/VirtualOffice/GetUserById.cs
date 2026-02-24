using Microsoft.AspNetCore.Http.HttpResults;
using Movement.Application.Network.Apis.VirtualOffice;
using Movement.Application.Network.Apis.VirtualOffice.Models;

namespace Movement.Api.Endpoints.VirtualOffice;

public static class GetUserById
{
    public const string Name = "GetUserById";
    public const string Path = "users/{id:int}";

    public static RouteHandlerBuilder MapGetUserByIdEndpoint(this RouteGroupBuilder group)
    {
        return group.MapGet(Path, Handler)
            .WithName(Name);

    }

    private static async Task<Results<Ok<User>, NotFound>> Handler(int id, IVirtualOfficeHttpService virtualOfficeHttpService, CancellationToken cancellationToken)
    {
        var result = await virtualOfficeHttpService.GetUserById(id, cancellationToken);

        return TypedResults.Ok(result);
    }
}