using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Infrastructure.Interfaces
{
    public interface IConsumerTemplate<TKey, TValue>
    {
        void ConsumeMessages(CancellationToken cancellationToken);
    }
}
