using GameFramework.Actor.Behaviours;
using GameFramework.Actor.DataModel;
using GameFramework.Actor.View;
using GameFramework.Collections;
using GameFramework.DataModel;
using GameFramework.Graph;
using GameFramework.Messaging;

namespace GameFramework.Actor
{
    public class Actor : IBehaviour
    {
        
        private StateMachine<Actor> _stateMachine;
        public readonly ServiceLocator<IBehaviour> Behaviors;
        public readonly Events Events;

        public readonly ActorView View;
        public readonly IWorld World;


        public Actor(ActorData entityData, ActorView entityView, IWorld world)
        {
            View = entityView;
            World = world;
            Events = new Events();
            Behaviors = new ServiceLocator<IBehaviour>(entityData.Behaviours.Count);
            Builder builder = new(this, Behaviors);
            for (int index = 0; index < entityData.Behaviours.Count; index++)
            {
                IData bd = entityData.Behaviours[index];
                bd.Accept(builder);
            }

            Behaviors.Build();
        }
        
        public void SetLogicGraph(StateMachine<Actor> logicGraph)
        {
            _stateMachine = logicGraph;
        }

        public void Update()
        {
            
            _stateMachine.DeltaTime = World.DeltaTime;
            Behaviors.ForEach( b =>
            {
                Behaviour behaviour = (Behaviour) b;  
                behaviour.Update(World.DeltaTime);
            });
            _stateMachine.Execute();
        }
    }
}