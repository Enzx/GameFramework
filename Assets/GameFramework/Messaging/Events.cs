using System;
using System.Collections.Generic;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation

namespace GameFramework.Messaging
{
    public class Events
    {
        private readonly Dictionary<Type, IPublisher> _brokers;

        public Events()
        {
            _brokers = new Dictionary<Type, IPublisher>(10);
        }

        public bool Publish<TMessage>(TMessage message)
        {
            if (!_brokers.TryGetValue(message.GetType(), out IPublisher publisher)) return false;
            if (publisher is IPublisher<TMessage> templatePublisher)
            {
                templatePublisher.Publish(message);
                return true;
            }
            publisher.Publish(message);
            return true;
        }

        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            if (_brokers.TryGetValue(typeof(TMessage), out IPublisher pub))
            {
                Broker<TMessage> broker = (Broker<TMessage>)pub;
                broker.Subscribe(handler);
            }
            else
            {
                Broker<TMessage> broker = new();
                broker.Subscribe(handler);
                _brokers.Add(typeof(TMessage), broker);
            }
        }
    }
}