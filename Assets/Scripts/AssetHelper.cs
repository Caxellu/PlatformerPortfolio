#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public static class AssetHelper
{
#if UNITY_EDITOR
    public static void SaveSOToAsset<T>(T so, string path) where T : ScriptableObject
    {
        AssetDatabase.CreateAsset(so, path);
        EditorUtility.SetDirty(so);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("ScriptableObject сохранён: " + path);
    }
#endif
}
