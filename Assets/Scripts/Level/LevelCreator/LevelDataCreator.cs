using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LevelCreator
{
    public class LevelDataCreator
    {
        public void CreateLevelData(int levelIndex, List<TileDTO> tile, List<TileDTO> tileBack, List<TileBase> uniqueTileBase,
            List<EnemyDTO> enemyDTOs, Vector3 spawnPlayerPos, WinColliderDTO winColliderDTO, PolygonColliderDTO polygonColliderDTO)
        {
            LevelSO levelData = ScriptableObject.CreateInstance<LevelSO>();
            int index = 1;
            string path;
            do
            {
                path = Path.Combine("Assets/Prefabs/Levels/", $"{"Level  " + index}.asset").Replace("\\", "/");
                index++;
            }
            while (File.Exists(path));

            levelData.LevelIndex = levelIndex;
            levelData.tiles = tile;
            levelData.tilesback = tileBack;
            levelData.uniqueTileBase = uniqueTileBase;
            levelData.enemyDTOs = enemyDTOs;
            levelData.spawnPlayerPos = spawnPlayerPos;
            levelData.winColliderDTO = winColliderDTO;
            levelData.polygonColliderDTO = polygonColliderDTO;

#if UNITY_EDITOR
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = levelData;
            AssetHelper.SaveSOToAsset(levelData, path);
            Debug.Log("ScriptableObject сохранён: " + path);
#endif
        }
    }
}