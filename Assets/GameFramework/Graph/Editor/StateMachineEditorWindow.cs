using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


namespace GameFramework.Graph.Editor
{
    public class StateMachineEditorWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset _graphViewAsset;
        [SerializeField] private StyleSheet _graphViewStyle;

        private StateMachineGraph _graphView;
        private Toolbar _toolbar;
        private ToolbarMenu _toolbarMenu;

        [MenuItem("Window/State Machine Editor")]
        public static void OpenWindow()
        {
            StateMachineEditorWindow window = GetWindow<StateMachineEditorWindow>();
        }

        private void CreateGUI()
        {
            
            
            titleContent = new GUIContent("State Machine Editor");
            _graphViewAsset.CloneTree(rootVisualElement);
            //Set up the graph view
            _graphView = rootVisualElement.Q<StateMachineGraph>();
            _graphView.styleSheets.Add(_graphViewStyle);
            _graphView.Init(this);
            //Set up the toolbar
            _toolbar = rootVisualElement.Q<Toolbar>();
            _toolbarMenu = _toolbar.Q<ToolbarMenu>();
            _toolbarMenu.menu.AppendAction("Open", OpenGraph);
            _toolbarMenu.menu.AppendAction("New", NewGraph);
            _toolbarMenu.menu.AppendAction("Save", SaveGraph);
            
    
        }

        private void OpenGraph(DropdownMenuAction obj)
        {
            string path = EditorUtility.OpenFilePanel("Open State Machine", "Assets", "asset");
            if (string.IsNullOrEmpty(path)) return;
            // make path relative to the project
            path = path.Replace(Application.dataPath, "Assets");
            StateMachineData data = AssetDatabase.LoadAssetAtPath<StateMachineData>(path);
            if (data == null) return;
            SerializedObject so = new(data);
            rootVisualElement.Bind(so);
            _graphView.SetGraphData(data);
             Label label = _toolbar.Q<Label>("graph-name-label");
             label.BindProperty(so.FindProperty("m_Name"));
        }
        

        private void SaveGraph(DropdownMenuAction action)
        {
            if (_graphView.GraphData == null) return;
            EditorUtility.SetDirty(_graphView.GraphData);
            AssetDatabase.SaveAssets();
        }

        private void NewGraph(DropdownMenuAction action)
        {
            StateMachineData data = CreateInstance<StateMachineData>();
            //Open Save File Dialog
            string path = EditorUtility.SaveFilePanelInProject("Save State Machine", "New State Machine", "asset",
                "Save State Machine", "Assets");
            if (string.IsNullOrEmpty(path)) return;
            AssetDatabase.CreateAsset(data, path);
            AssetDatabase.SaveAssetIfDirty(data);
            _graphView.SetGraphData(data);
            
        }
    }
}