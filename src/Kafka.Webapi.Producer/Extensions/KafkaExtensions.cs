using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Kafka.Infrastructure.Config;

namespace Kafka.Webapi.Producer.Extensions
{
    public static class KafkaExtensions
    {
        public static IServiceCollection ConfigAdmin(this IServiceCollection services)
        {
            var config = KafkaAdminConfig.Config();
            var adminClient = KafkaAdminConfig.GetAdminClient(config);

            services.AddSingleton<IAdminClient>(provider => adminClient);

            KafkaAdminConfig.CreateTopics(adminClient, "str-topic", 2, 1);

            return services;
        }
    }
}