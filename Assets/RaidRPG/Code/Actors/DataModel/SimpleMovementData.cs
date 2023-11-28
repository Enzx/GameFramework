using GameFramework.Actor.DataModel;
using UnityEngine;

namespace RaidRPG
{
    [CreateAssetMenu(fileName = "SimpleMovementData", menuName = "RaidRPG/ActorData/SimpleMovementData")]
    public class SimpleMovementData : ObjectData<SimpleMovement, IMovement>
    {
        public Speed Speed;
    }
}