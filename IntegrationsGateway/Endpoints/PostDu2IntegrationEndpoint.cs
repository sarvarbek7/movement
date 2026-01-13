using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Movement.IntegrationsGateway.Requests;
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
                                                                                                   RabbitMqConnectionManager rabbitMqConnectionManager,
                                                                                                   ILogger<PostDu2Integration> logger,
                                                                                                   CancellationToken cancellationToken)
    {
        try
        {
            IConnection connection = await rabbitMqConnectionManager.GetConnectionAsync("movement_backend",
                cancellationToken);

            IChannel channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            var basicProperties = new BasicProperties()
            {
                ContentType = "application/json",
                Persistent = true,
                ContentEncoding = "UTF-8"
            };


            string jsonText = JsonSerializer.Serialize(request);
            ReadOnlyMemory<byte> body = Encoding.UTF8.GetBytes(jsonText);

            await channel.BasicPublishAsync("amq.direct",
                                            ROUTING_KEY,
                                            mandatory: true,
                                            basicProperties,
                                            body,
                                            cancellationToken);

            await channel.CloseAsync(cancellationToken);
            await channel.DisposeAsync();

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