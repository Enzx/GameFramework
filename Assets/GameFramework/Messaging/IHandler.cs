namespace GameFramework.Messaging
{
    public interface IHandler<T>
    {
        void Handle(T message);
        void Dispose();
        bool Filter(T message);
    }
}