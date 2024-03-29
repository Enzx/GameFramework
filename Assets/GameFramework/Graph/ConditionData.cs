using System.Collections.Generic;

namespace GameFramework.Graph
{
    public class ConditionData : NodeData
    {
        public static readonly ConditionData Default = new();
        public List<ConditionTask> Conditions = new();
        public ExecuteMode ExecuteMode = ExecuteMode.Sequence;
    }
}