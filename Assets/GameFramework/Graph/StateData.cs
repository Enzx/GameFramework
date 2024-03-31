using System;
using System.Collections.Generic;
using GameFramework.DataModel;

namespace GameFramework.Graph
{
    public class StateData : NodeData
    {
        public List<ActionTask> Actions;
        public override IObject Accept(IDataVisitor dataVisitor)
        {
            return dataVisitor.Visit<State>(this);
        }
    }
}