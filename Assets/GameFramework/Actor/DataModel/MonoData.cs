using GameFramework.Actor.Behaviours;
using UnityEngine;

namespace GameFramework.Actor.DataModel
{
    public abstract class MonoData<TBehavior, TInterface> : MonoData where TBehavior : IBehaviour
    {
        public sealed override IBehaviour Accept(IDataVisitor dataVisitor)
        {
            return dataVisitor.Visit<TBehavior, TInterface>(this);
        }
    }

    public abstract class MonoData<TBehavior> : MonoData where TBehavior : IBehaviour
    {
        public sealed override IBehaviour Accept(IDataVisitor dataVisitor)
        {
            return dataVisitor.Visit<TBehavior>(this);
        }
    }

    public abstract class MonoData : MonoBehaviour, IData
    {
        public abstract IBehaviour Accept(IDataVisitor dataVisitor);
    }
}