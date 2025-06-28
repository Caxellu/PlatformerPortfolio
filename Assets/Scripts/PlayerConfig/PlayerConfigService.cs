using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerConfigService 
{
    private PlayerConfigSO config;
    public PlayerConfigSO Config => config;
    public async UniTask InitializeAsync()
    {
        config = await LoadConfig("PlayerConfig");
    }
    private async UniTask<PlayerConfigSO> LoadConfig(string key)
    {
        var handle = Addressables.LoadAssetsAsync<PlayerConfigSO>(key, null);
        await handle.ToUniTask();

        if (handle.Status == AsyncOperationStatus.Succeeded)
            return handle.Result.ToList()[0];

        Debug.LogError($"Failed to load assets with label: {key}");
        return null;
    }
}
