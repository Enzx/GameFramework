using GameFramework.Actor;
using UnityEngine;
using Behaviour = GameFramework.Actor.Behaviours.Behaviour;

namespace RaidRPG
{
    public class InputBehaviour : Behaviour
    {
        private readonly InputData _data;

        public InputBehaviour(Actor actor, InputData data) : base(actor, data)
        {
            _data = data;
        }

        public override void Update(float deltaTime)
        {
            InputMessage message = default;
            message.MoveDirection.x = Input.GetAxis(_data.HorizontalAxis);
            message.MoveDirection.y = Input.GetAxis(_data.VerticalAxis);
            if (Input.GetKey(_data.FireKey))
            {
                message.Fire = true;
            }

            Actor.Events.Publish(message);
        }
    }
}