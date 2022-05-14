using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace JKang.EventBus.AmazonSqs
{
    public class AmazonSqsEventPublisher : IEventPublisher
    {
        private readonly IEventSerializer _eventSerializer;
        private readonly IOptions<AmazonSqsEventPublisherOptions> _options;

        public AmazonSqsEventPublisher(
            IEventSerializer eventSerializer,
            IOptions<AmazonSqsEventPublisherOptions> options)
        {
            _eventSerializer = eventSerializer;
            _options = options;
        }

        public async Task PublishEventAsync<TEvent>(TEvent @event)
        {
            var regionEndpoint = RegionEndpoint.GetBySystemName(_options.Value.Region);
            var client = new AmazonSQSClient(_options.Value.AccessKeyId, _options.Value.SecretAccessKey, regionEndpoint);

            AmazonSqsQueueAttribute attr = typeof(TEvent)
                .GetCustomAttributes(typeof(AmazonSqsQueueAttribute), inherit: false)
                .OfType<AmazonSqsQueueAttribute>()
                .FirstOrDefault();

            string queueUrl = attr?.Url ?? _options.Value.DefaultQueueUrl;

            var request = new SendMessageRequest()
            {
                MessageBody = _eventSerializer.Serialize(@event),
                QueueUrl = queueUrl
            };

            var result = await client.SendMessageAsync(request);
        }
    }
}
