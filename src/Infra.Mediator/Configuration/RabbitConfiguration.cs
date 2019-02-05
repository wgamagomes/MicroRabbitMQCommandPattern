using System.Configuration;

namespace Infra.Mediator
{
    /// <summary>
    /// Each specific service should be provide their own configuration.
    /// </summary>
    public class RabbitSection : ConfigurationSection
    {
        public static RabbitSection Section { get { return ConfigurationManager.GetSection("rabbit") as RabbitSection; } }

        [ConfigurationProperty("settings")]
        public RabbitSettings Settings
        {
            get
            {
                return (RabbitSettings)this["settings"];
            }
        }
    }

    public class RabbitSettings : ConfigurationElement
    {

        [ConfigurationProperty("user")]
        public string User
        {
            get
            {
                return (string)this["user"];
            }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get
            {
                return (string)this["password"];
            }
        }

        [ConfigurationProperty("host")]
        public string Host
        {
            get
            {
                return (string)this["host"];
            }
        }

        [ConfigurationProperty("queue")]
        public string Queue
        {
            get
            {
                return (string)this["queue"];
            }
        }

        [ConfigurationProperty("exchange")]
        public string Exchange
        {
            get
            {
                return (string)this["exchange"];
            }
        }

        [ConfigurationProperty("routingKey")]
        public string RoutingKey
        {
            get
            {
                return (string)this["routingKey"];
            }
        }
    }
}