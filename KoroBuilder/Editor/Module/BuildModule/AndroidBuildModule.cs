using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KoroBuilder
{
    public class AndroidBuildModule : IBuildModule
    {
        public string Name => "AndroidV1";

        private bool _useSign;

        public void Build()
        {
            List<string> scenes = new List<string>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                if (EditorBuildSettings.scenes[i].enabled)
                {
                    scenes.Add(EditorBuildSettings.scenes[i].path);
                }
            }

            BuildPipeline.BuildPlayer(scenes.ToArray(), "./Builds/Android/Build.apk", BuildTarget.Android, BuildOptions.None);
        }

        public void OnGUI()
        {
            _useSign = EditorGUILayout.Toggle("Use sign", _useSign);
            if (_useSign)
            {
                if (GUILayout.Button("Key")) EditorUtility.OpenFilePanel("", "", "keystore");
                EditorGUILayout.LabelField("Path: ");
                EditorGUILayout.PasswordField("");
            }
        }
    }
}
