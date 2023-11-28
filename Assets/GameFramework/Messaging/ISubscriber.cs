namespace GameFramework.Messaging
{
    public interface ISubscriber<T>
    {
        void Subscribe(IHandler<T> message);
    }
}