using GameFramework.Actor.DataModel;

namespace GameFramework.Actor.Behaviours
{
    public abstract class Behaviour : IBehaviour, IUpdate 
    {
        protected Actor Actor;
        protected IData Data;

        protected Behaviour(Actor actor, IData data)
        {
            Actor = actor;
            Data = data;
        }

        public virtual void Update(float deltaTime)
        {
        }
    
    }
}