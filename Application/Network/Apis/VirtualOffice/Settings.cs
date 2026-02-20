namespace Movement.Application.Network.Apis.VirtualOffice;

public class VirtualOfficeApiSettings
{
    public const string SectionName = "VirtualOfficeApiSettings";

    public string BaseUrl { get; init; } = default!;
    public VirtualOfficeApiEndpoints Endpoints { get; init; } = default!;
}

public class VirtualOfficeApiEndpoints
{
    public string GetUserByPinfl { get; init; } = default!;
    public string GetUserById { get; init; } = default!;
    public string GetDepartmentById { get; init; } = default!;
    public string GetWorkplaceById { get; init; } = default!;
    public string GetStationByCode { get; init; } = default!;
}
