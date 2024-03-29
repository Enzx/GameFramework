using System;
using UnityEngine;

namespace GameFramework.Graph
{
    public class NodeData : ScriptableObject
    {
        public NodeId Key = new() { Id = SerializableGuid.NewGuid() };
    }
}