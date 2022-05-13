using System;

namespace JKang.EventBus.AmazonSqs
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AmazonSqsQueueAttribute: Attribute
    {
        public AmazonSqsQueueAttribute(string url)
        {
            Url = url;
        }

        public string Url { get; }
    }
}
