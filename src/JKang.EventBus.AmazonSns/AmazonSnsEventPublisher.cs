using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JKang.EventBus.AmazonSns
{
    public class AmazonSnsEventPublisher : IEventPublisher
    {
        private readonly IEventSerializer _eventSerializer;
        private readonly IOptions<AmazonSnsEventPublisherOptions> _options;

        public AmazonSnsEventPublisher(
            IEventSerializer eventSerializer,
            IOptions<AmazonSnsEventPublisherOptions> options)
        {
            _eventSerializer = eventSerializer;
            _options = options;
        }

        public async Task PublishEventAsync<TEvent>(TEvent @event, int delaySeconds = 0)
        {
            var endpoint = RegionEndpoint.GetBySystemName(_options.Value.Region);
            var client = new AmazonSimpleNotificationServiceClient(endpoint);

            Type eventType = typeof(TEvent);
            AmazonSnsTopicAttribute attr = eventType
                .GetCustomAttributes(typeof(AmazonSnsTopicAttribute), inherit: false)
                .OfType<AmazonSnsTopicAttribute>()
                .FirstOrDefault();
            string topicName = attr == null
                ? eventType.FullName.ToLower().Replace('.', '-')
                : attr.Name;
            var createTopicRequest = new CreateTopicRequest(topicName);
            CreateTopicResponse createTopicResponse = await client.CreateTopicAsync(createTopicRequest);
            string topicArn = createTopicResponse.TopicArn;

            string message = _eventSerializer.Serialize(@event);
            var publishRequest = new PublishRequest(topicArn, message);

            if(delaySeconds > 0)
                await Task.Delay(delaySeconds * 1000);
            PublishResponse publishResponse = await client.PublishAsync(publishRequest);
        }
    }
}
