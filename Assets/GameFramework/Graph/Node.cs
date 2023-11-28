namespace GameFramework.Graph
{
    public abstract class Node
    {
        public NodeId Id;
        public abstract Result Execute();
    }
}