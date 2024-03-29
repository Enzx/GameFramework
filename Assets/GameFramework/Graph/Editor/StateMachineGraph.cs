using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameFramework.Graph.Editor
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, UxmlTraits>
        {
        }
    }

    public class StateMachineGraph : GraphView
    {
        StateMachineEditorWindow _window;
        private Vector2 _mouseWorldPosition;
        private Vector2 _mouseLocalPosition;

        //Expose StateMachineGraph uxml
        public new class UxmlFactory : UxmlFactory<StateMachineGraph, UxmlTraits>
        {
        }

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

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Add State", AddState, DropdownMenuAction.AlwaysEnabled);
        }

        private void AddState(DropdownMenuAction obj)
        {
            StateNode stateNode = new();
            stateNode.SetPosition(new Rect(_mouseLocalPosition, Vector2.one));
            stateNode.title = "State";
            AddElement(stateNode);
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