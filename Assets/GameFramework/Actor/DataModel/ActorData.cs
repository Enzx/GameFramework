using System.Collections.Generic;
using GameFramework.DataModel;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameFramework.Actor.DataModel
{
    [CreateAssetMenu(fileName = "ActorData", menuName = "GameFramework/ActorData/ActorData")]
    public class ActorData : ObjectData<Actor>
    {
        public AssetReference ViewReference;
        [SerializeReference] public List<ObjectData> Behaviours;
    }
}