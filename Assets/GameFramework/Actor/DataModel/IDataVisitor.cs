using GameFramework.Actor.Behaviours;

namespace GameFramework.Actor.DataModel
{
    public interface IDataVisitor
    {
        IBehaviour Visit<TBehavior>(IData data);
        IBehaviour Visit<TBehavior, TInterface>(IData data);
    }
}