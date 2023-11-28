using GameFramework.Actor.Behaviours;
using UnityEngine;

namespace RaidRPG
{
    public interface IMovement : IBehaviour
    {
        void Move(Vector2 direction);
    }
}