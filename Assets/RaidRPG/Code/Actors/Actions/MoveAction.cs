using GameFramework.Actor;
using GameFramework.Graph;

namespace RaidRPG.Actors.Actions
{
    public class MoveAction : ActionTask<Actor>
    {
        private IMovement _movement;

        private void InputOnMove(InputMessage data)
        {
            _movement.Move(data.MoveDirection);
        }

        public override Result Execute()
        {
            _movement = Agent.Behaviors.Get<IMovement>();
            if (_movement == null)
            {
                ReportError("Actor does not have IMovement behaviour");
                return Result.Failure;
            }
            Agent.Events.Subscribe<InputMessage>(InputOnMove);
            return Result.Success;
        }
    }
}