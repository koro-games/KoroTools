using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoroBuilder
{
    public interface IBuildModule
    {
        string Name { get; }
        void OnGUI();
        void Build();
    }
}
