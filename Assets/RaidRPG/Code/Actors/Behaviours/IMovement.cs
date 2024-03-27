using GameFramework.Actor.Behaviours;
using UnityEngine;

namespace RaidRPG
{
    public interface IDash : IBehaviour
    {
        void Dash(Vector2 direction);
    }
}