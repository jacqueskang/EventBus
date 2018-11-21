using System;

namespace JKang.EventBus.AmazonSns
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AmazonSnsTopicAttribute: Attribute
    {
        public AmazonSnsTopicAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
