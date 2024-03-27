using System.Collections.Generic;
using System.Linq;
using GameFramework.Graph;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Behaviour = GameFramework.Actor.Behaviours.Behaviour;
using Node = UnityEditor.Experimental.GraphView.Node;


namespace GameFramework.Editor
{
    public class StateMachineEditorWindow : EditorWindow
    {
        //Create a new window
        [MenuItem("Window/State Machine Editor")]
        public static void OpenWindow()
        {
            //Create a new window
            StateMachineEditorWindow window = GetWindow<StateMachineEditorWindow>();
            //Set a title for the window
            window.titleContent = new GUIContent("State Machine Editor");
            //Create a new state machine graph
            StateMachineGraph graphView = new StateMachineGraph();
            //Add the graph view to the window
            window.rootVisualElement.Add(graphView);
            graphView.StretchToParentSize();
            //Initialize the graph view
            graphView.Init(window);
        }
    }


    public class StateMachineGraph : GraphView
    {
        StateMachineEditorWindow _window;

        //Create a new graph view
        public StateMachineGraph()
        {
        }

        //Initialize the graph view
        public void Init(StateMachineEditorWindow window)
        {
            //Set the window
            _window = window;
            //Create a grid background
            GridBackground gridBackground = new GridBackground();
            //Add the grid background to the graph view
            Insert(0, gridBackground);
            //Set the grid background to the graph view
            gridBackground.StretchToParentSize();
            //Create a zoomable container
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            //Create a pannable container
            this.AddManipulator(new ContentDragger());
            //Create a selection container
            this.AddManipulator(new SelectionDragger());
            //Create a context menu
            this.AddManipulator(new ContextualMenuManipulator(BuildContextualMenu));
            //Create a mini map
            MiniMap miniMap = new MiniMap();
            //Add the mini map to the graph view
            miniMap.SetPosition(new Rect(10, 30, 200, 140));
            //Add the mini map to the graph view
            Add(miniMap);
            //Create a blackboard
            Blackboard blackboard = new Blackboard(this);
            //Add the blackboard to the graph view
            blackboard.Add(new BlackboardSection { title = "Exposed Properties" });
            //Add the blackboard to the graph view
            blackboard.SetPosition(new Rect(10, 200, 200, 300));
            //Add the blackboard to the graph view
            Add(blackboard);
        }

        //Build the context menu
        private void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            //Create a new menu item
            evt.menu.AppendAction("Add State", AddState, DropdownMenuAction.AlwaysEnabled);
        }

        private void AddState(DropdownMenuAction obj)
        {
            //Create a new node for a new state
            StateNode stateNode = new StateNode();
            //Set the position of the node using the mouse position
            stateNode.SetPosition(new Rect(GetMousePosition().x, GetMousePosition().y, 100, 100));
            //Set the title of the node
            stateNode.title = "State";
            //Add the node to the graph view
            AddElement(stateNode);
        }

        //Get compatible ports for a node to connect to another node
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            //Create a list of ports
            List<Port> compatiblePorts = new();
            //Get all the ports in the graph view
            ports.ForEach(port =>
            {
                //Check if the port is not the same as the start port
                if (startPort != port && startPort.node != port.node)
                {
                    //Add the port to the list
                    compatiblePorts.Add(port);
                }
            });
            //Return the list of ports
            return compatiblePorts;
        }

        //Get the mouse position relative to the window
        private Vector2 GetMousePosition()
        {
            //Get the mouse position
            Vector2 mousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent,
                _window.rootVisualElement.worldBound.center - _window.position.position);
            return mousePosition;
        }
    }

    internal class StateNode : Node
    {
        private readonly State _state;

        //Add rename functionality to the node 
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Rename", (a) => { OpenTextEditor(); }, DropdownMenuAction.AlwaysEnabled);
        }

        //Rename the node if F2 is pressed or if the node title is double clicked
        public override void OnSelected()
        {
            base.OnSelected();
            if (Event.current.keyCode == KeyCode.F2 || Event.current.clickCount == 2)
            {
                OpenTextEditor();
            }
        }


        //Add ports to the node
        public StateNode()
        {
            _state = new State();
            //Create a port for the node
            Port inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi,
                typeof(float));
            //Set the name of the port
            inputPort.portName = "Input";
            //Add the port to the node
            inputContainer.Add(inputPort);
            //Create a port for the node
            Port outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi,
                typeof(float));
            //Set the name of the port
            outputPort.portName = "Output";
            //Add the port to the node
            outputContainer.Add(outputPort);

            //Create a button
            Button button = new Button(() => { AddAction(); });
            //Set the text of the button
            button.text = "Add Action";
            //Add the button to the node
            mainContainer.Add(button);

            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<ActionTask>();
            Dictionary<string, System.Type> typeDictionary = new();
            foreach (System.Type type in types)
            {
                typeDictionary.Add(type.Name, type);
            }
            PopupField<string> popupField = new(typeDictionary.Keys.ToList(),  0, key =>
            {
                mainContainer.  Add(new Label(key));
                return key;
            });
            mainContainer.Add(popupField);
        }

        private void AddAction()
        {
        }


        private void OpenTextEditor()
        {
            //Create a text field
            TextField textField = new TextField();
            //Set the text field value to the title of the node
            textField.SetValueWithoutNotify(title);
            //Set the text field to be on focus
            textField.Focus();
            //Set the text field to be selected
            textField.SelectAll();
            //Add the text field to the node
            mainContainer.Add(textField);
            //Set the text field to be on focus
            textField.Q(TextField.textInputUssName).Focus();
            //Set the text field to be selected
            textField.Q(TextField.textInputUssName);
            textField.SelectAll();
            //Add a callback to the text field
            textField.RegisterCallback<FocusOutEvent>(evt =>
            {
                //Remove the text field from the node
                mainContainer.Remove(textField);
                //Set the title of the node to the text field value
                title = textField.value;
            });
        }
    }
}