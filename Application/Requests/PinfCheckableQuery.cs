using Mediator;
using Movement.Domain.Users.Entities;

namespace Movement.Application.Requests;

public abstract class PinfCheckableQuery<TResponse>(string pinfl) : IQuery<TResponse>
{
    public string Pinfl { get; init; } = pinfl;

    public User? User { get; private set; }

    public void SetUser(User user)
    {
        User = user;
    }
}