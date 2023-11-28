using GameFramework.Actor;
using UnityEngine;
using Behaviour = GameFramework.Actor.Behaviours.Behaviour;

namespace RaidRPG
{
    public class SwimmingMovement : Behaviour, IMovement
    {
        public SwimmingMovement(Actor actor, SwimmingMovementData data) : base(actor, data)
        {
        }

        public void Move(Vector2 direction)
        {
        }
    }
}