using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace JKang.Events.RabbitMq
{
    public class RabbitMqEventBus : IEventPublisher
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqEventBus()
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
        }

        public async Task PublishEventAsync(IEvent @event)
        {
            await Task.Run(() =>
            {
                using (IConnection connection = _factory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string serailiazed = JsonConvert.SerializeObject(@event);
                    byte[] body = Encoding.UTF8.GetBytes(serailiazed);

                    channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                }
            });
        }
    }
}
