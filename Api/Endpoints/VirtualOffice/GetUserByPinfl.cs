using Mediator;
using Microsoft.AspNetCore.Http.HttpResults;
using Movement.Application.Network.Apis.VirtualOffice;
using Movement.Application.Network.Apis.VirtualOffice.Models;

namespace Movement.Api.Endpoints.VirtualOffice;

public static class GetUserByPinfl
{
    public const string Name = "GetUserByPinfl";
    public const string Path = "user/{pinfl}";

    public static RouteHandlerBuilder MapGetUserByPinflEndpoint(this RouteGroupBuilder group)
    {
        return group.MapGet(Path, Handler)
            .WithName(Name);

    }

    private static async Task<Results<Ok<User>, NotFound>> Handler(string pinfl, IVirtualOfficeHttpService virtualOfficeHttpService, ISender sender, CancellationToken cancellationToken)
    {
        var result = await virtualOfficeHttpService.GetUserByPinfl(pinfl, cancellationToken);

        return TypedResults.Ok(result);
    }
}