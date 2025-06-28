using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GlobalGameController : MonoBehaviour, IInitializable
{
    [Inject] private EnemyService _enemyService;
    public async void Initialize()
    {
        //Init Components
        await UniTask.WhenAll(
            _enemyService.InitializeAsync()
            );
    }
}
