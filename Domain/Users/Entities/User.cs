using Shared.Entities;

namespace Movement.Domain.Users.Entities;

public class User : IEntity<int>, ISoftDeleted, IHasExternalId<int>
{
    private User() { }

    public int Id { get; init; }
    public int ExternalId { get; private set; }
    public bool IsDeleted { get; private set; }
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string? MiddleName { get; init; }
    public string? Phone { get; init; }
    public string? Image { get; init; }
    public string Pinfl { get; init; } = default!;
    public Position[] Positions { get; init; } = [];
    public Role[] Roles { get; init; } = [];

    public static User Create(int externalId, string firstName, string lastName, string? middleName, string? phone, string? image, string pinfl)
    {
        return new User
        {
            ExternalId = externalId,
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            Phone = phone,
            Image = image,
            Pinfl = pinfl
        };
    }
}