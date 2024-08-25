using UnityEngine;

namespace GameFramework.Graph
{
    public abstract class ConditionTask : ScriptableObject
    {
        public abstract bool Check();
    }
}