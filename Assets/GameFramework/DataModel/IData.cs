using GameFramework.Actor.Behaviours;

namespace GameFramework.DataModel
{
    public interface IData
    {
        IObject Accept(IDataVisitor dataVisitor);
    }
}