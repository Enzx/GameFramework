using System;
using System.Linq;
using RaidRPG.Actors.Actions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameFramework.Graph.Editor
{
    [CustomEditor(typeof(StateData))]
    public class StateDataEditor : UnityEditor.Editor
    {
        private StateData _stateNode;
        public VisualTreeAsset _stateDataEditorUxml;

        private void OnEnable()
        {
            _stateNode = (StateData)target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            _stateNode.Actions.Clear();
            for (int i = 0; i < 5; i++)
            {
                if (i % 2 == 0)
                    _stateNode.Actions.Add(CreateInstance<PrintInfoAction>());
                else
                {
                    _stateNode.Actions.Add(CreateInstance<BindInputAction>());
                }
            }

            VisualElement root = new();
            _stateDataEditorUxml.CloneTree(root);
            ListView list = root.Q<ListView>("actions-list");
            list.makeItem = () => new VisualElement();
            list.bindItem = (e, i) =>
            {
                FillDefaultInspector(e, new SerializedObject(_stateNode.Actions[i]));
                // InspectorElement inspectorElement = new(_stateNode.Actions[i]);
                // e.Add(inspectorElement);
                //Hide m_Script field
            };

            return root;
        }

        private static void FillDefaultInspector(VisualElement container, SerializedObject serializedObject)
        {
            if (serializedObject == null)
                return;
            SerializedProperty iterator = serializedObject.GetIterator();
            if (iterator.NextVisible(true))
            {
                do
                {
                    if (iterator.propertyPath == "m_Script")
                        continue;
                    Type type = serializedObject.targetObject.GetType();
                    Label label = new(ObjectNames.NicifyVariableName(type.Name));
                    container.Add(label);
                    
                    PropertyField propertyField = new(iterator)
                    {
                        name = "PropertyField:" + iterator.propertyPath
                    };
                    propertyField.AddToClassList("unity-disabled unity-property-field__inspector-property");

                    container.Add(propertyField);
                } while (iterator.NextVisible(false));
            }

            container.Bind(serializedObject);
        }
    }
}