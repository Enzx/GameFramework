using GameFramework.Actor.DataModel;
using GameFramework.DataModel;
using RaidRPG.Actors.Behaviours;
using UnityEngine;

namespace RaidRPG.Actors.DataModel
{
    [CreateAssetMenu(menuName = "RaidRPG/ActorData/AttackData", fileName = "AttackData", order = 0)]
    public class AttackData : ObjectData<WeaponBehaviour, IWeapon>
    {
        public float FireRate;
        public float Damage;
        public Bullet BulletPrefab;
    }
}