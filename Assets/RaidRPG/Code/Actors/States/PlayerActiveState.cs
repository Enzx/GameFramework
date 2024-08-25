using GameFramework.Actor;
using GameFramework.Graph;

namespace RaidRPG.Actors.States
{
    public class PlayerActiveState : State<Actor>
    {
        private InputBehaviour _input;

        protected override void OnEnter()
        {
            _input = Agent.Behaviors.Get<InputBehaviour>();
        }


        protected override void OnUpdate(float deltaTime)
        {
            _input.Update(deltaTime);
        }

        protected override void OnExit()
        {

        }
    }
}
