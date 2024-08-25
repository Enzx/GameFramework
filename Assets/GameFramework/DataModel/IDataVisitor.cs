using GameFramework.Actor.Behaviours;

namespace GameFramework.DataModel
{
    public interface IDataVisitor
    {
        IObject Visit<TObject>(IData data);
        IObject Visit<TObject, TInterface>(IData data);
    }
}