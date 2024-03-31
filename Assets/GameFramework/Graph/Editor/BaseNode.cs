using System.Collections.Generic;
using System.Linq;
using RaidRPG.Actors.Actions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameFramework.Graph.Editor
{
    public abstract class BaseNode : UnityEditor.Experimental.GraphView.Node
    {
        protected GraphView GraphView => _graphView ??= GetFirstAncestorOfType<GraphView>();
        protected Port InputPort;
        protected Port OutputPort;

        protected readonly NodeData Data;
        private GraphView _graphView;
        private Label _titleLabel;
        private TextField _renameTextField = new() { name = "rename-textField", isDelayed = true };


        protected BaseNode(NodeData data)
        {
            Data = data;
            focusable = true;
            pickingMode = PickingMode.Position;
            capabilities |= Capabilities.Renamable |
                            Capabilities.Deletable |
                            Capabilities.Copiable |
                            Capabilities.Movable;

            AddPorts();

            SetupRenameTextEditor();

            RegisterCallback<KeyDownEvent>(OnKeyDownEvent, TrickleDown.TrickleDown);
            titleContainer.RegisterCallback<MouseDownEvent>(MouseRename, TrickleDown.TrickleDown);
        }

        void SetupRenameTextEditor()
        {
            if (_titleLabel != null) return;
            _titleLabel = titleContainer.Q<Label>("title-label");
            _renameTextField = new TextField
            {
                name = "rename-textField", isDelayed = true,
                style =
                {
                    fontSize = _titleLabel.style.fontSize,
                    paddingTop = 8.5f,
                    paddingLeft = 4f,
                    paddingRight = 4f,
                    paddingBottom = 7.5f
                }
            };
            _renameTextField.ElementAt(0).style.height = 18f;
        }

        private void MouseRename(MouseDownEvent evt)
        {
            if (evt.clickCount == 2 && evt.button == (int)MouseButton.LeftMouse && IsRenamable())
            {
                OpenTextEditor();
            }
        }

        private void OnKeyDownEvent(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.F2 && IsRenamable())
            {
                OpenTextEditor();
            }
        }

        private void AddPorts()
        {
            InputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
                typeof(BaseNode));
            InputPort.portName = "";
            inputContainer.Add(InputPort);

            OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(BaseNode));
            OutputPort.portName = "";
            outputContainer.Add(OutputPort);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
            evt.menu.AppendAction("Rename",
                _ => { OpenTextEditor(); },
                _ => IsRenamable() ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
        }


        private void OpenTextEditor()
        {
            _renameTextField.SetValueWithoutNotify(title);
            _renameTextField.Focus();
            _renameTextField.SelectAll();

            _titleLabel.style.display = DisplayStyle.None;

            titleContainer.Insert(1, _renameTextField);
            _renameTextField.Q(TextField.textInputUssName).Focus();
            _renameTextField.Q(TextField.textInputUssName);
            _renameTextField.SelectAll();
            _renameTextField.RegisterCallback<FocusOutEvent>(_ =>
            {
                _titleLabel.style.display = DisplayStyle.Flex;
                titleContainer.Remove(_renameTextField);
                title = _renameTextField.value;
            });
            return;
        }
    }

    public class StateNode : BaseNode
    {
        public StateNode(StateData data) : base(data)
        {
            data.Actions = new List<ActionTask> { ScriptableObject.CreateInstance<PrintInfoAction>() };
        }
    }
}