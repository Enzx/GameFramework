using System;

namespace GameFramework.Messaging
{
    public class Handler<T> : IHandler<T>, IDisposable
    {
        private Action<T> _callback;
        private readonly Filter<T> _filter;

        public Handler(Action<T> callback, Filter<T> filter)
        {
            _callback = callback;
            _filter = filter;
        }

        public void Dispose()
        {
            _callback = null;
        }

        public bool Filter(T message)
        {
            return _filter == default || _filter.Apply(message);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void Handle(T message)
        {
            _callback(message);
        }
    }
}