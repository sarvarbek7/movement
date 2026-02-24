using Microsoft.AspNetCore.Http.HttpResults;
using Movement.Application.Network.Apis.VirtualOffice;
using Movement.Application.Network.Apis.VirtualOffice.Models;

namespace Movement.Api.Endpoints.VirtualOffice;

public static class GetStationByCode
{
    public const string Name = "GetStationByCode";
    public const string Path = "station/{code}";

    public static RouteHandlerBuilder MapGetStationByCodeEndpoint(this RouteGroupBuilder group)
    {
        return group.MapGet(Path, Handler)
            .WithName(Name);

    }

    private static async Task<Results<Ok<Department>, NotFound>> Handler(string code, IVirtualOfficeHttpService virtualOfficeHttpService, CancellationToken cancellationToken)
    {
        var result = await virtualOfficeHttpService.GetStationByCode(code, cancellationToken);

        return TypedResults.Ok(result);
    }
}