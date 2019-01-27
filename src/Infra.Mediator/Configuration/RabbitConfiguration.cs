namespace Infra.Mediator
{
    /// <summary>
    /// Each specific service should be provide their own configuration.
    /// </summary>
    public static class RabbitConfiguration
    {
        public static string User => "guest";
        public static string Password => "guest";
        public static string Host => "localhost";
        public static string QueueName =>"queue-test";
        public static string ExchangeName => "exchange-test";
        public static string RoutingKey => "routing-key-test.*";
    }
}
