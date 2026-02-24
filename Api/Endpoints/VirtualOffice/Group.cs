namespace Movement.Api.Endpoints.VirtualOffice;

public static class Group
{
    public const string Name = "VirtualOffice";
    public const string BasePath = "virtual-office";

    public static RouteGroupBuilder MapVirtualOfficeEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup(BasePath).WithTags(Name);

        group.MapGetUserByPinflEndpoint();
        group.MapGetUserByIdEndpoint();
        group.MapGetWorkplaceByIdEndpoint();
        group.MapGetDepartmentByIdEndpoint();
        group.MapGetStationByCodeEndpoint();


        return group;
    }
}