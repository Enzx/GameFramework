using System;
using GameFramework.DataModel;

namespace GameFramework.Graph
{
    public class NodeData : ObjectData
    {
        private void Awake()
        {
            Key = new NodeId { Id = SerializableGuid.NewGuid() };
        }

        public NodeId Key;

        public override IObject Accept(IDataVisitor dataVisitor)
        {
            return dataVisitor.Visit<Node>(this);
        }

        public static NodeData Create<TData>() where TData : NodeData
        {
            return CreateInstance<TData>();
        }

        public static NodeData Create(Type type)
        {
            return (NodeData)CreateInstance(type);
        }
    }
}