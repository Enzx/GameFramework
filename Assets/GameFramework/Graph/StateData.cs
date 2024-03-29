using System;
using System.Collections.Generic;

namespace GameFramework.Graph
{
    public class StateData : NodeData
    {
        public static readonly StateData Default = new();
        public List<ActionTask> Actions = new();

    }
}