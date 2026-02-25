using Mediator;
using Movement.Application.Test.Queries;

namespace Movement.Infrastructure.Test.QueryHandlers;

public class TestQueryHandler : IQueryHandler<TestQuery, string>
{
    public ValueTask<string> Handle(TestQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}