using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KoroBuilder
{
    public class iOSBuildModule : IBuildModule
    {
        public string Name => "iOSV1";

        public void Build()
        {
            EditorUserBuildSettings.symlinkLibraries = false;
            EditorUserBuildSettings.development = false;
            EditorUserBuildSettings.allowDebugging = false;

            List<string> scenes = new List<string>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                if (EditorBuildSettings.scenes[i].enabled)
                {
                    scenes.Add(EditorBuildSettings.scenes[i].path);
                }
            }
            Terminal.ExecuteProcessTerminal($"cd {Application.dataPath}/Builds/iOS/");
            Terminal.ExecuteProcessTerminal($"cd chgchgvkjhv");
            Terminal.ExecuteProcessTerminal("echo $CDPATH");
            //BuildPipeline.BuildPlayer(scenes.ToArray(), "./Builds/iOS/", BuildTarget.iOS, BuildOptions.None);
        }

        public void OnGUI()
        {

        }
    }
}
