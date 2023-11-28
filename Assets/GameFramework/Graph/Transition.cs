namespace GameFramework.Graph
{
    public struct Transition
    {
        public readonly NodeId Source;
        public readonly NodeId Destination;

        public Transition(NodeId from, NodeId to)
        {
            Source = from;
            Destination = to;
        }
    }
}