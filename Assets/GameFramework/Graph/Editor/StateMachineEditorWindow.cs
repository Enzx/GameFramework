using System;
using UnityEditor;
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
}