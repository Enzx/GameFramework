using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace GameFramework.Graph.Editor
{
    public class StateMachineGraph : GraphView
    {
        public new class UxmlFactory : UxmlFactory<StateMachineGraph, UxmlTraits>
        {
        }

        public GraphData GraphData => _graphData;

        private StateMachineEditorWindow _window;
        private Vector2 _mouseWorldPosition;
        private Vector2 _mouseLocalPosition;

        private GraphData _graphData;

        private readonly NodeFactory _nodeFactory;

        //Expose StateMachineGraph uxml


        public StateMachineGraph()
        {
            this.StretchToParentSize();
            GridBackground gridBackground = new();
            Insert(0, gridBackground);
            gridBackground.SendToBack();
            gridBackground.StretchToParentSize();
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new ContextualMenuManipulator(BuildContextualMenu));

            //Create a mini map
            MiniMap miniMap = new();
            miniMap.SetPosition(new Rect(10, 30, 200, 140));
            Add(miniMap);


            //Create a blackboard
            Blackboard blackboard = new(this);
            blackboard.Add(new BlackboardSection { title = "Exposed Properties" });
            blackboard.SetPosition(new Rect(10, 200, 200, 300));
            Add(blackboard);

            RegisterCallback<MouseUpEvent>(MouseUp);

            nodeCreationRequest = NodeCreationRequest;

            _nodeFactory = new NodeFactory();
        }


        private void NodeCreationRequest(NodeCreationContext ctx)
        {
            AddNode(typeof(StateData));
        }

        public void SetGraphData(GraphData graphData)
        {
            _graphData = graphData;
        }

        private void MouseUp(MouseUpEvent evt)
        {
            _mouseWorldPosition = evt.mousePosition;
            _mouseLocalPosition = contentViewContainer.WorldToLocal(_mouseWorldPosition);
        }


        public void Init(StateMachineEditorWindow window)
        {
            _window = window;
        }


        private void AddNode(Type type)
        {
            NodeData nodeData = NodeData.Create(type);
            BaseNode node = _nodeFactory.Create(nodeData);
            node.SetPosition(new Rect(_mouseLocalPosition, Vector2.one));
            AddElement(node);
            _graphData.NodesData ??= new List<NodeData>();
            _graphData.NodesData.Add(nodeData);
            nodeData.name = node.title;
            AssetDatabase.AddObjectToAsset(nodeData, _graphData);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new();
            ports.ForEach(port =>
            {
                if (startPort != port && startPort.node != port.node)
                {
                    compatiblePorts.Add(port);
                }
            });
            return compatiblePorts;
        }
    }
}