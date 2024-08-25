using GameFramework.Actor;
using GameFramework.Actor.Behaviours;
using UnityEngine;
using Behaviour = GameFramework.Actor.Behaviours.Behaviour;

namespace RaidRPG
{

    public interface IMovement : IBehaviour
    {
        public void Move(Vector2 direction);

    }
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