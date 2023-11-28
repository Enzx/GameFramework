using System;
using System.Collections.Generic;

namespace GameFramework.Graph
{
    public class Graph
    {
        //Transitions are edges between nodes
        private List<Transition> _transitions;
        private Dictionary<NodeId, Node> _nodes;
        private Node _startNode;

        public Graph(Node startNode)
        {
            _startNode = startNode;

            _transitions = new List<Transition>();
            _nodes = new Dictionary<NodeId, Node> { { _startNode.Id, _startNode } };
        }


        public void AddNode(Node node)
        {
            _nodes.Add(node.Id, node);
        }

//Create a transition from a node to another and store it
        public void AddTransition(NodeId from, NodeId to)
        {
            Transition transition = new(from, to);
            _transitions.Add(transition);
        }

        public Node this[NodeId currentState] => _nodes[currentState];

        public Transition GetTransition(NodeId currentState)
        {
            for (int index = 0; index < _transitions.Count; index++)
            {
                Transition transition = _transitions[index];
                if (transition.Source.Equals(currentState))
                {
                    return transition;
                }
            }

            throw new Exception("Transition not found");
        }
    }
}