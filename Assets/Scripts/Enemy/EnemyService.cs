using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyService 
{
    private List<EnemySO> enemyList= new List<EnemySO>();
    public List<EnemySO> EnemyList=> enemyList;
    public async UniTask InitializeAsync()
    {
        enemyList = await LoadEnemySo("Enemy");
    }
    private async UniTask<List<EnemySO>> LoadEnemySo(string key)
    {
        var handle = Addressables.LoadAssetsAsync<EnemySO>(key, null);
        await handle.ToUniTask();

        if (handle.Status == AsyncOperationStatus.Succeeded)
            return handle.Result.ToList();

        Debug.LogError($"Failed to load assets with label: {key}");
        return null;
    }
}
