namespace JKang.EventBus.AmazonSqs
{
    public class AmazonSqsEventPublisherOptions
    {
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }
        public string DefaultQueueUrl { get; set; }
        public string Region { get; set; } = "us-east-1";
    }
}
