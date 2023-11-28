using GameFramework.Actor.DataModel;
using UnityEngine;

namespace RaidRPG
{
    [CreateAssetMenu(fileName = "InputData", menuName = "RaidRPG/ActorData/InputData")]
    public class InputData : ObjectData<InputBehaviour>
    {
        public string HorizontalAxis;
        public string VerticalAxis;
        public KeyCode FireKey;
    }
}