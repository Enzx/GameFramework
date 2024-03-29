namespace GameFramework.Graph
{
    public abstract class Node
    {
        public NodeId Key => _data.Key;

      private readonly NodeData _data;

      protected Node(NodeData data)
      {
          _data = data;
      }

      public abstract Result Execute();
    }
}