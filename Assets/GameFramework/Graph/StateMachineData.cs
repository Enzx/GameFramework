using System.Collections.Generic;

namespace GameFramework.Graph
{
    public class StateMachineData : StateData
    {
        public List<NodeData> Nodes = new();
        public List<Transition> Transitions = new();
    }
}