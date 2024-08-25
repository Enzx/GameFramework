using System;
using System.Collections.Generic;
using GameFramework.DataModel;
using UnityEngine;

namespace GameFramework.Graph
{
    public class StateData : NodeData
    {
        [SerializeReference] public List<ActionTask> Actions;

        public override IObject Accept(IDataVisitor dataVisitor)
        {
            return dataVisitor.Visit<State>(this);
        }
    }
}