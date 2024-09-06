using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Kafka.Infrastructure.Config;
using Kafka.Infrastructure.Interfaces;
using Kafka.Infrastructure.Producers;
using Kafka.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace Kafka.Webapi.Producer.Extensions
{
    public static class KafkaExtensions
    {
        public static IServiceCollection ConfigAdmin(this IServiceCollection services)
        {
            var config = KafkaAdminConfig.AdminConfig();
            var adminClient = KafkaAdminConfig.GetAdminClient(config);

            services.AddSingleton<IAdminClient>(provider => adminClient);

            KafkaAdminConfig.CreateTopics(adminClient, "str-topic", 2, 1);

            return services;
        }

        public static IServiceCollection ConfigProducer(this IServiceCollection services)
        {
            services.AddScoped<IProducer<string, string>>(provider =>
            {
                var config = KafkaAdminConfig.ProducerConfig();

                return new ProducerBuilder<string, string>(config)
                    .SetKeySerializer(Serializers.Utf8)
                    .SetValueSerializer(Serializers.Utf8)
                    .Build();
            });

            services.AddScoped(typeof(IKafkaTemplate<,>), typeof(KafkaTemplate<,>));

            return services;
        }
    }
}