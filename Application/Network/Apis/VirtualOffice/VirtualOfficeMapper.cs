using Movement.Domain.Users.Entities;

namespace Movement.Application.OrchestartionServices.Users;

public static class VirtualOfficeMapper
{
    public static User MapToUser(this Network.Apis.VirtualOffice.Models.User virtualOfficeUser)
    {
        return User.Create(externalId: virtualOfficeUser.Id,
                           firstName: virtualOfficeUser.FirstName,
                           lastName: virtualOfficeUser.LastName,
                           middleName: virtualOfficeUser.MiddleName,
                           phone: virtualOfficeUser.Phone,
                           image: virtualOfficeUser.Image,
                           pinfl: virtualOfficeUser.Pinfl);
    }

    public static Workplace MapToWorkplace(this Network.Apis.VirtualOffice.Models.Workplace virtualOfficeWorkplace)
    {
        return Workplace.Create(externalId: virtualOfficeWorkplace.Id,
                                name: virtualOfficeWorkplace.Name,
                                parentId: virtualOfficeWorkplace.ParentId);
    }

    public static Department MapToDepartment(this Network.Apis.VirtualOffice.Models.Department virtualOfficeDepartment, int workplaceId)
    {
        return Department.Create(externalId: virtualOfficeDepartment.Id,
                                 name: virtualOfficeDepartment.Name,
                                 level: virtualOfficeDepartment.Level,
                                 code: virtualOfficeDepartment.Code,
                                 workplaceId: workplaceId);
    }
}
