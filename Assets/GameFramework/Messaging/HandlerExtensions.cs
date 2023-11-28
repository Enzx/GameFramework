using System;

namespace GameFramework.Messaging
{
    public static class HandlerExtensions
    {
        public static void Subscribe<T>(this ISubscriber<T> subscriber, Action<T> action)
        {
            subscriber.Subscribe(new Handler<T>(action, null));
        }

        public static void Subscribe<T>(this ISubscriber<T> subscriber, Action<T> action, Filter<T> filter)
        {
            subscriber.Subscribe(new Handler<T>(action, filter));
        }
    }
}