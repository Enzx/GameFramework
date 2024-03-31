using GameFramework.Actor.DataModel;
using GameFramework.DataModel;
using UnityEngine;

namespace RaidRPG
{
    [CreateAssetMenu(fileName = "SwimmingMovementData", menuName = "RaidRPG/ActorData/SwimmingMovementData")]
    public class SwimmingMovementData : ObjectData<SwimmingMovement, IDash>
    {
        public float Speed;
    }
}