using MassTransit;
using MassTransit.DependencyInjection;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movement.IntegrationsGateway.Requests;
using Movement.MessagingContracts.Buses;
using RabbitMQ.Client;

namespace Movement.IntegrationsGateway.Endpoints;

public static class PostDu2IntegrationEndpoint
{
    const string ROUTING_KEY = "enakl.du2s.state";

    public static void MapPostDu2TimeIntegration(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("api/du2s/integrations/time", PostDu2TimeIntegrationHandler)
                    .WithTags("Du2s");
    }

    private static async Task<Results<NoContent, ProblemHttpResult>> PostDu2TimeIntegrationHandler([FromBody] PostDu2Integration request,
                                                                                                   Bind<IMovementBackendBus, IPublishEndpoint> bus,
                                                                                                   ILogger<PostDu2Integration> logger,
                                                                                                   CancellationToken cancellationToken)
    {
        try
        {
            var postDu2Event = request.ToEvent();

            await bus.Value.Publish(postDu2Event, cancellationToken);

            logger.LogInformation("Published PostDu2IntegionEvent: {@event}", postDu2Event);

            return TypedResults.NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while posting du2 time.");

            logger.LogCritical(message: "Unhandled exception. Request: {request}", request.ToString());

            return TypedResults.NoContent();
        }
    }
}