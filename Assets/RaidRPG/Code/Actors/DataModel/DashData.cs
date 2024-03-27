using GameFramework.Actor.DataModel;
using UnityEngine;

namespace RaidRPG
{
    [CreateAssetMenu(fileName = "Dash", menuName = "RaidRPG/ActorData/DashData")]
    public class DashData : ObjectData<DashBehaviour, IDash>
    {

    }
}