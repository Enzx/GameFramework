using GameFramework.Actor;
using GameFramework.Graph;

namespace RaidRPG.Actors.Actions
{
    public class BindInputAction : ActionTask<Actor>
    {
        private IMovement _movement;
        private IWeapon _weapon;
        [UnityEngine.Range(0, 10)]
        public float Speed = 5f;

   

        public override Result Execute()
        {
            _movement = Agent.Behaviors.Get<IMovement>();
            _weapon = Agent.Behaviors.Get<IWeapon>();
            if (_movement == null)
            {
                ReportError("Actor does not have IMovement behaviour");
                return Result.Failure;
            }
            Agent.Events.Subscribe<InputMessage>(OnReceiveInput);
            return Result.Success;
        }
        
        private void OnReceiveInput(InputMessage data)
        {
            _movement.Move(data.MoveDirection);
            _weapon.SetDirection(data.MouseWorldPosition);
            if (data.Fire)
            {
                _weapon.Shoot();
            }
        }
    }
}