using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System.Collections.Concurrent;
using System.Xml.Linq;

namespace Kafka.Infrastructure.Config
{
    public class KafkaAdminConfig
    {
        public static AdminClientConfig AdminConfig()
            => new()
            {
                BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_CONNECTION")
            };

        public static ProducerConfig ProducerConfig()
            => new()
            {
                BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_CONNECTION"),
            };

        public static ConsumerConfig ConsumerConfig()
            => new()
            {
                BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_CONNECTION"),
                GroupId = "group-1",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

        public static IAdminClient GetAdminClient(AdminClientConfig config)
            => new AdminClientBuilder(config).Build();

        public static async Task CreateTopics(IAdminClient adminClient, string name, int partitions, short replicas)
        {
            var topicSpecifications = new List<TopicSpecification>
            {
                new()
                {
                    Name = name,
                    NumPartitions = partitions,
                    ReplicationFactor = replicas
                }
            };

            await adminClient.CreateTopicsAsync(topicSpecifications);
        }
    }
}
