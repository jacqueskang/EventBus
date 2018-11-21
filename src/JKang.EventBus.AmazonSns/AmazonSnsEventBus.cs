using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace JKang.EventBus.AmazonSns
{
    public class AmazonSnsEventBus : IEventPublisher
    {
        private readonly IEventSerializer _eventSerializer;
        private readonly IOptions<AmazonSnsEventBusOptions> _options;

        public AmazonSnsEventBus(
            IEventSerializer eventSerializer,
            IOptions<AmazonSnsEventBusOptions> options)
        {
            _eventSerializer = eventSerializer;
            _options = options;
        }

        public async Task PublishEventAsync<TEvent>(TEvent @event)
        {
            var endpoint = RegionEndpoint.GetBySystemName(_options.Value.Region);
            var client = new AmazonSimpleNotificationServiceClient(endpoint);
            string topicName = typeof(TEvent).FullName.ToLower().Replace('.', '-');
            var createTopicRequest = new CreateTopicRequest(topicName);
            CreateTopicResponse createTopicResponse = await client.CreateTopicAsync(createTopicRequest);
            string topicArn = createTopicResponse.TopicArn;

            string message = _eventSerializer.Serialize(@event);
            var publishRequest = new PublishRequest(topicArn, message);
            PublishResponse publishResponse = await client.PublishAsync(publishRequest);
        }
    }
}
