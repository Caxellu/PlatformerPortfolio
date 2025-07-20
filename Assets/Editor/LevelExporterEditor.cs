using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LevelCreator
{
    [CustomEditor(typeof(LevelExporter))]
    public class LevelExporterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelExporter demo = (LevelExporter)target;

            if (GUILayout.Button("Save Level"))
            {
                demo.ExportLevel();
            }
        }

    }
}