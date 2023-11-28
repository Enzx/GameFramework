using GameFramework.Actor.Behaviours;
using UnityEngine;

namespace GameFramework.Actor.DataModel
{
    public abstract class ObjectData<TBehavior, TInterface> : ObjectData where TBehavior : IBehaviour
    {
        public sealed override IBehaviour Accept(IDataVisitor dataVisitor)
        {
            return dataVisitor.Visit<TBehavior, TInterface>(this);
        }
    }

    public abstract class ObjectData<TBehavior> : ObjectData where TBehavior : IBehaviour
    {
        public sealed override IBehaviour Accept(IDataVisitor dataVisitor)
        {
            return dataVisitor.Visit<TBehavior>(this);
        }
    }

    public abstract class ObjectData : ScriptableObject, IData
    {
        public abstract IBehaviour Accept(IDataVisitor dataVisitor);
    }
}