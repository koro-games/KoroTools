using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KoroGames.FolderCreator
{
    public class FolderCreator
    {
        [MenuItem("Tools/KoroFolders/FolderTamplate/Create Tamplate Folders")]
        static void CreateTamplateFolders()
        {
            CreateFolder("Materials");
            CreateFolder("Mashes");
            CreateFolder("Plugins");
            CreateFolder("Prefabs");
            CreateFolder("Resources");
            CreateFolder("Scenes");
            CreateFolder("Sprites");
            CreateFolder("Audio");
            CreateFolder("Animations");
            CreateFolder("StorePackages");
            CreateFolder("Scripts");

            CreateFolder("UI", "Assets/Scripts");
            CreateFolder("Ads", "Assets/Scripts");
            CreateFolder("Balance", "Assets/Scripts");
            CreateFolder("Utility", "Assets/Scripts");
            CreateFolder("World", "Assets/Scripts");
            CreateFolder("Test", "Assets/Scripts");
        }


        private static void CreateFolder(string name, string parent = "Assets")
        {
            if (!AssetDatabase.IsValidFolder($"{parent}/{name}"))
                AssetDatabase.CreateFolder(parent, name);
        }
    }
}
