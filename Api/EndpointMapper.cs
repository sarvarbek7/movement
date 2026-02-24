using Movement.Api.Endpoints.VirtualOffice;

namespace Movement.Api;

public static class EndpointMapper
{
    public static void MapEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("api").WithTags("Api");

        api.MapVirtualOfficeEndpoints();
    }
}