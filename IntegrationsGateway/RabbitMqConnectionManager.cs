using RabbitMQ.Client;

namespace Movement.IntegrationsGateway;


public sealed class RabbitMqConnectionManager(IConfiguration configuration) : IDisposable, IAsyncDisposable
{
    private readonly Dictionary<string, IConnection> connections = [];

    public async Task<IConnection> GetConnectionAsync(string name,
         CancellationToken cancellationToken = default)
    {
        IConnection? connection = null;

        if (connections.TryGetValue(name, out var value))
        {
            connection = value!;
        }

        if (connection is null || !connection.IsOpen)
        {
            var mq = configuration.GetSection("Mq")
                ?? throw new InvalidOperationException();

            if (name == "movement_backend")
            {
                var movementBackend = mq.GetSection("movement_backend")
                    ?? throw new InvalidOperationException();

                var factory = new ConnectionFactory
                {
                    HostName = movementBackend.GetValue<string>("host")
                        ?? throw new InvalidOperationException(),

                    Port = movementBackend.GetValue<int?>("port")
                        ?? throw new InvalidOperationException(),

                    UserName = movementBackend.GetValue<string>("username")
                        ?? throw new InvalidOperationException(),

                    Password = movementBackend.GetValue<string>("password")
                        ?? throw new InvalidOperationException(),

                    VirtualHost = movementBackend.GetValue<string>("vhost")
                        ?? throw new InvalidOperationException(),
                };

                var newConnection = await factory.CreateConnectionAsync(clientProvidedName: name,
                    cancellationToken);

                connections[name] = newConnection;

                return newConnection;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        return connection;
    }

    public void Dispose()
    {
        foreach (IConnection connection in connections.Values)
        {
            connection.Dispose();
        }

        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        foreach (IConnection connection in connections.Values)
        {
            await connection.CloseAsync();
        }

        foreach (IConnection connection in connections.Values)
        {
            await connection.DisposeAsync();
        }
    }
}
