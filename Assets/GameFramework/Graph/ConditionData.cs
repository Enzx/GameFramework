using System.Collections.Generic;

namespace GameFramework.Graph
{
    public class ConditionData : NodeData
    {
        public List<ConditionTask> Conditions = new();
        public ExecuteMode ExecuteMode = ExecuteMode.Sequence;
    }
}