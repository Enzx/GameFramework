using System;
using System.Collections.Generic;
using System.Linq;
using RaidRPG.Actors.Actions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


namespace GameFramework.Graph.Editor
{
    public class StateMachineEditorWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset _graphViewAsset;
        [SerializeField] private StyleSheet _graphViewStyle;
        
        [MenuItem("Window/State Machine Editor")]
        public static void OpenWindow()
        {
            StateMachineEditorWindow window = GetWindow<StateMachineEditorWindow>();
      
        }

        private void CreateGUI()
        {
            titleContent = new GUIContent("State Machine Editor");
            _graphViewAsset.CloneTree(rootVisualElement);
            StateMachineGraph graphView = rootVisualElement.Q<StateMachineGraph>();
            graphView.styleSheets.Add(_graphViewStyle);
            
            graphView.Init(this);
        }
    }

    internal class StateNode : UnityEditor.Experimental.GraphView.Node
    {
        private readonly StateData _state;

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Rename", (a) => { OpenTextEditor(); }, DropdownMenuAction.AlwaysEnabled);
        }

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
            _state = new StateData();
            Port inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi,
                typeof(float));
            inputPort.portName = "Input";
            inputContainer.Add(inputPort);
            Port outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi,
                typeof(float));
            outputPort.portName = "Output";
            outputContainer.Add(outputPort);

            Button button = new(AddAction)
            {
                text = "Add Action"
            };
            mainContainer.Add(button);

            TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<ActionTask>();
            Dictionary<string, System.Type> typeDictionary = new();
            for (int i = 0; i < types.Count; i++)
            {
                System.Type type = types[i];
                typeDictionary.Add(type.Name, type);
            }

            PopupField<string> popupField = new(typeDictionary.Keys.ToList(), 0, key =>
            {
                mainContainer.Add(new Label(key));
                return key;
            });
            mainContainer.Add(popupField);
        }

        private void AddAction()
        {
            _state.Actions.Add(ScriptableObject.CreateInstance<PrintInfoAction>());
            
        }


        private void OpenTextEditor()
        {
            TextField textField = new();
            textField.SetValueWithoutNotify(title);
            textField.Focus();
            textField.SelectAll();
            mainContainer.Add(textField);
            textField.Q(TextField.textInputUssName).Focus();
            textField.Q(TextField.textInputUssName);
            textField.SelectAll();
            textField.RegisterCallback<FocusOutEvent>(_ =>
            {
                mainContainer.Remove(textField);
                title = textField.value;
            });
        }
    }
}