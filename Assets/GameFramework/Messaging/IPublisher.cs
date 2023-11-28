namespace GameFramework.Messaging
{
    public interface IPublisher
    {
        void Publish(object obj);
    }
    public interface IPublisher<T> : IPublisher
    {
        void Publish(T message);
    }
}