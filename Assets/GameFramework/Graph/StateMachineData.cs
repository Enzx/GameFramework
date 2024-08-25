using System.Collections.Generic;
using GameFramework.DataModel;

namespace GameFramework.Graph
{
    public class StateMachineData : GraphData
    {
        public override IObject Accept(IDataVisitor dataVisitor)
        {
            return dataVisitor.Visit<StateMachine>(this);
        }
    }
}