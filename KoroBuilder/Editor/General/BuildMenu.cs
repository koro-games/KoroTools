using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace KoroBuilder
{

    public class BuildMenu : EditorWindow
    {

        private static IBuildModule[] Modules = { new iOSBuildModule(), new AndroidBuildModule() };
        private static IBuildModule CurrentModule;
        private static int currentModuleId;

        private static int CurrentModuleId { get => currentModuleId; set { SetCurrentModule(value); currentModuleId = value; } }

        private string[] _modulesName;

        [MenuItem("Tools/Koro/Builder/Setting")]
        private static void ShowWindow()
        {
            var window = GetWindow<BuildMenu>();
            window.titleContent = new GUIContent("BuildMenu");
            window.Show();
        }

        [MenuItem("Tools/Koro/Builder/Build module")]
        private static void BuildCurrentModule()
        {
            Debug.Log($"Build {CurrentModule.Name}");
            CurrentModule.Build();
        }

        private static void SetCurrentModule(int id)
        {
            CurrentModule = Modules[id];
        }

        private void OnEnable()
        {
            SetCurrentModule(0);
            _modulesName = Modules.Select(item => item.Name).ToList().ToArray();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Build Module");
            CurrentModuleId = EditorGUILayout.Popup(CurrentModuleId, _modulesName);
            DrawLine();

            EditorGUILayout.LabelField("Build Setting");
            CurrentModule.OnGUI();
            DrawLine();
            if (GUILayout.Button("Build"))
            {
                BuildCurrentModule();
            }
        }

        private void DrawLine()
        {
            Rect rect = EditorGUILayout.GetControlRect(false, 1);
            rect.height = 1;
            GUI.color = GUI.backgroundColor;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
            GUI.color = Color.white;
        }
    }
}


