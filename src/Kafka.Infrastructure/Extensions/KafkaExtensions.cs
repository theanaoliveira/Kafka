using Confluent.Kafka;
using Kafka.Infrastructure.Config;
using Kafka.Infrastructure.Consumers;
using Kafka.Infrastructure.Interfaces;
using Kafka.Infrastructure.Producers;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Infrastructure.Extensions
{
    public static class KafkaExtensions
    {
        public static IServiceCollection ConfigAdmin(this IServiceCollection services)
        {
            var config = KafkaAdminConfig.AdminConfig();
            var adminClient = KafkaAdminConfig.GetAdminClient(config);

            services.AddSingleton<IAdminClient>(provider => adminClient);

            var topic = Environment.GetEnvironmentVariable("TOPIC");

            if (!string.IsNullOrEmpty(topic))
                KafkaAdminConfig.CreateTopics(adminClient, topic, 2, 1);

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

            services.AddScoped(typeof(IProducerTemplate<,>), typeof(ProducerTemplate<,>));

            return services;
        }

        public static IServiceCollection ConfigConsumer(this IServiceCollection services)
        {
            services.AddScoped<IConsumer<string, string>>(provider =>
            {
                var config = KafkaAdminConfig.ConsumerConfig();

                return new ConsumerBuilder<string, string>(config)
                    .SetKeyDeserializer(Deserializers.Utf8)
                    .SetValueDeserializer(Deserializers.Utf8)
                    .Build();
            });

            services.AddScoped(typeof(IConsumerTemplate<,>), typeof(ConsumerTemplate<,>));

            return services;
        }
    }
}
