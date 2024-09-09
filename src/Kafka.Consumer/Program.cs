using Confluent.Kafka;
using Kafka.Infrastructure.Config;

var config = KafkaAdminConfig.ConsumerConfig();

using var consumer = new ConsumerBuilder<string, string>(config)
    .SetKeyDeserializer(Deserializers.Utf8)
    .SetValueDeserializer(Deserializers.Utf8)
    .Build();

consumer.Subscribe("str-topic");

Listener(consumer);

static void Listener(IConsumer<string, string> consumer)
{

    CancellationTokenSource cts = new CancellationTokenSource();

    Console.CancelKeyPress += (_, e) => {
        e.Cancel = true; // Prevent the process from terminating.
        cts.Cancel(); // Request cancellation.
    };

    try
    {
        while (!cts.Token.IsCancellationRequested)
        {
            try
            {
                var consumeResult = consumer.Consume(cts.Token);

                // Process the message
                Console.WriteLine($"Mensagem recebida: {consumeResult.Value}");
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Erro ao consumir mensagem: {e.Error.Reason}");
            }
        }
    }
    catch (OperationCanceledException)
    {
        consumer.Close(); // Ensure the consumer leaves the group cleanly and final offsets are committed.
    }
}