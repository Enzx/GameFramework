using System;

namespace GameFramework.Graph
{
    [Serializable]
    public class Transition
    {
        public NodeId Source;
        public NodeId Destination;
        
        
        public Transition(NodeId from, NodeId to)
        {
            Source = from;
            Destination = to;
        }
    }
}