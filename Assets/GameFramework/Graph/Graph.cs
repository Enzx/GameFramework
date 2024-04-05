using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework.Graph
{
    public class GraphData : NodeData
    {
        [SerializeReference] public NodeData StartNodeData;
        [SerializeReference] public List<NodeData> NodesData = new();
        [SerializeReference]  public List<Transition> Transitions = new();
    }

    public class Graph
    {
        //Transitions are edges between nodes
        public readonly List<Transition> Transitions;
        public readonly Dictionary<NodeId, Node> Nodes;

        public readonly Node StartNode;

        private NodeId _lastAddedNode;

        public Graph(Node startNode)
        {
            StartNode = startNode;
            _lastAddedNode = startNode.Key;
            Transitions = new List<Transition>();
            Nodes = new Dictionary<NodeId, Node> { { startNode.Key, startNode } };
        }

        public void SetAgent<TAgent>(TAgent agent)
        {
            foreach (KeyValuePair<NodeId, Node> pair in Nodes)
            {
                if (pair.Value is IAGentSettable<TAgent> agentSettable)
                {
                    agentSettable.SetAgent(agent);
                }
            }
        }


        public void AddNode(Node node)
        {
            if (Nodes.TryAdd(node.Key, node) == false)
            {
                throw new Exception($"Node ({node.GetType()}) already exists");
            }
        }

        public Graph ConnectTo(Node node)
        {
            AddNode(node);
            AddTransition(_lastAddedNode, node.Key);
            _lastAddedNode = node.Key;
            return this;
        }

        public void AddTransition(NodeId from, NodeId to)
        {
            Transition transition = new(from, to);
            Transitions.Add(transition);
        }


        public void AddTransitions(IEnumerable<Transition> transitions)
        {
            Transitions.AddRange(transitions);
        }

        public Node this[NodeId currentState] => Nodes[currentState];

        public Transition GetTransition(NodeId currentState)
        {
            for (int index = 0; index < Transitions.Count; index++)
            {
                Transition transition = Transitions[index];
                if (transition.Source.Equals(currentState))
                {
                    return transition;
                }
            }

            throw new Exception("Transition not found");
        }
    }
}