using GameFramework.Actor.Behaviours;
using UnityEngine;

namespace RaidRPG
{
    public interface IWeapon : IBehaviour
    {
        void Shoot();
        void SetDirection(Vector3 targetPosition);
    }
}