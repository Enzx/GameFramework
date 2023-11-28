namespace GameFramework.Graph
{
    public abstract class Node
    {
        public readonly NodeId Key = new()
        {
            Id = SerializableGuid.NewGuid()
        };

        public abstract Result Execute();
    }
}