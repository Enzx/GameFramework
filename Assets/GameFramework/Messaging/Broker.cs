using System;
using System.Collections.Generic;

namespace GameFramework.Messaging
{
    public class Broker<T> : IPublisher<T>, ISubscriber<T>, IDisposable
    {
        private readonly List<IHandler<T>> _handlers = new(128);

        public void Dispose()
        {
            for (int i = 0; i < _handlers.Count; i++)
            {
                _handlers[i].Dispose();
            }
        }

        public void Publish(T message)
        {
            for (int i = 0; i < _handlers.Count; i++)
            {
                if (_handlers[i].Filter(message))
                    _handlers[i].Handle(message);
            }
        }

        public void Subscribe(IHandler<T> handler)
        {
            if (_handlers.Contains(handler))
                return;

            _handlers.Add(handler);
        }

        public void Subscribe(Action<T> handler)
        {
            Subscribe(new Handler<T>(handler,null));
        }

        public void Publish(object obj)
        {
            Publish((T)obj);
        }
    }
}