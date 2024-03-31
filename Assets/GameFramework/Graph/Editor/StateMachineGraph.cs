using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameFramework.DataModel;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameFramework.Graph.Editor
{
    public class NodeFactory
    {
        private readonly Dictionary<Type, ConstructorInfo> _nodeTypeMap = new();

        public BaseNode Create(NodeData nodeData)
        {
            if (_nodeTypeMap.TryGetValue(nodeData.GetType(), out ConstructorInfo constructorInfo))
            {
                return Construct(constructorInfo, nodeData);
            }

            CacheConstructorInfo(out constructorInfo, nodeData);
            return Construct(constructorInfo, nodeData);
        }

        private void CacheConstructorInfo(out ConstructorInfo constructorInfo, NodeData nodeData)
        {
            constructorInfo = TypeCache.GetTypesDerivedFrom<BaseNode>()
                .Select(type => type.GetConstructor(new[] { nodeData.GetType() }))
                .FirstOrDefault();

            if (constructorInfo == null)
            {
                throw new Exception($"No constructor found for {nodeData.GetType()}");
            }

            _nodeTypeMap.Add(nodeData.GetType(), constructorInfo);
        }

        private static BaseNode Construct(ConstructorInfo constructorInfo, NodeData nodeData)
        {
            BaseNode node = (BaseNode)constructorInfo.Invoke(new object[] { nodeData });
            node.title = node.GetType().Name;
            return node;
        }
    }

    public class StateMachineGraph : GraphView
    {
        public new class UxmlFactory : UxmlFactory<StateMachineGraph, UxmlTraits>
        {
        }

        StateMachineEditorWindow _window;
        private Vector2 _mouseWorldPosition;
        private Vector2 _mouseLocalPosition;

        private GraphData _graphData;
        private string _dataPath;

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
            _dataPath = AssetDatabase.GetAssetPath(_graphData);
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
            //     _graphData.NodesData.Add(nodeData);
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