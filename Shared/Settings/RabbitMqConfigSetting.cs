using Shared.Interfaces;

namespace Shared.Settings;

public class RabbitMqConfigSetting : IHostPortUsernamePassword
{
    /// <summary>
    /// The RabbitMQ host address. Default is "localhost"
    /// </summary>
    public string Host  {get; set;} = "localhost";

    /// <summary>
    /// The RabbitMQ virtual host. Default is "/"
    /// </summary>
    public string VHost { get; set; } = "/";

    /// <summary>
    /// The RabbitMQ port. Default is 5672
    /// </summary>
    public short Port  {get; set;} = 5672;

    /// <summary>
    /// The RabbitMQ username. Default is "guest"
    /// </summary>
    public string Username  {get; set;} = "guest";


    /// <summary>
    /// The RabbitMQ password. Default is "guest"
    /// </summary>
    public string Password  {get; set;} = "guest";
}