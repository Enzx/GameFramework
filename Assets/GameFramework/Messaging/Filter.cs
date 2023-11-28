namespace GameFramework.Messaging
{
    public abstract class Filter<TMessage>
    {
        public abstract bool Apply(TMessage message);
    }
}