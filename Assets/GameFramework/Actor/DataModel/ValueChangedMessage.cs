namespace GameFramework.Actor.DataModel
{
    public struct ValueChangedMessage<T>
    {
        public T PreviousValue;
        public T CurrentValue;
    }
}