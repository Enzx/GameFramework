using GameFramework.Actor.Behaviours;

namespace GameFramework.Actor.DataModel
{
    public interface IData
    {
        IBehaviour Accept(IDataVisitor dataVisitor);
    }
}