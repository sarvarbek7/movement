using Shared.Interfaces;

namespace Shared.Settings;

public record RabbitMqConfigSetting : IHostPortUsernamePassword
{
    /// <summary>
    /// The RabbitMQ host address. Default is "localhost"
    /// </summary>
    public string Host  {get; init;} = "localhost";

    /// <summary>
    /// The RabbitMQ virtual host. Default is "/"
    /// </summary>
    public string VirtualHost { get; init; } = "/";

    /// <summary>
    /// The RabbitMQ port. Default is 5672
    /// </summary>
    public short Port  {get; init;} = 5672;

    /// <summary>
    /// The RabbitMQ username. Default is "guest"
    /// </summary>
    public string Username  {get; init;} = "guest";


    /// <summary>
    /// The RabbitMQ password. Default is "guest"
    /// </summary>
    public string Password  {get; init;} = "guest";
}