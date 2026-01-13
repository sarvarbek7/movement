using MassTransit;
using Movement.MessagingContracts.Enakl;

namespace Movement.Api.Consumers;

public sealed class PostDu2Consumer(ILogger<PostDu2Consumer> logger) : IConsumer<PostDu2IntegionEvent>
{
    public Task Consume(ConsumeContext<PostDu2IntegionEvent> context)
    {
        logger.LogInformation("Received PostDu2IntegionEvent: {@event}", context.Message);
        return Task.CompletedTask;
    }
}