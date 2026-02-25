using Mediator;
using Movement.Application.Test.Queries;

namespace Movement.Infrastructure.Test.QueryHandlers;

public class TestQueryHandler : IQueryHandler<TestQuery, string>
{
    public async ValueTask<string> Handle(TestQuery query, CancellationToken cancellationToken)
    {
        await Task.Yield(); // Simulate asynchronous work, such as database access or external API calls.

        return query.Pinfl;
    }
}