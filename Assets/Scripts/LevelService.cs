using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelService
{
    private List<LevelSO> levelList = new List<LevelSO>();
    public LevelSO CurrentLevelSo => currentLevelSo;
    private LevelSO currentLevelSo;
    public bool IsAvailablenextLevel=>currentLevelIndex+1 < levelList.Count;
    private int currentLevelIndex;
    public async UniTask InitializeAsync()
    {
        levelList = await LoadLevelSo("Level");
        currentLevelIndex= 0;
        currentLevelSo = levelList[currentLevelIndex];
    }
    public void NextLevel()
    {
        currentLevelIndex = Mathf.Clamp(currentLevelIndex++, 0, levelList.Count);
        currentLevelSo = levelList[currentLevelIndex];
    }
    private async UniTask<List<LevelSO>> LoadLevelSo(string key)
    {
        var handle = Addressables.LoadAssetsAsync<LevelSO>(key, null);
        await handle.ToUniTask();

        if (handle.Status == AsyncOperationStatus.Succeeded)
            return handle.Result.ToList();

        Debug.LogError($"Failed to load assets with label: {key}");
        return null;
    }
}
