using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GlobalGameController : MonoBehaviour, IInitializable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private EnemyService _enemyService;
    [Inject] private LevelService _levelService;
    [Inject] private PlayerConfigService _playerConfigService;
    public async void Initialize()
    {
        //Init Components
        await UniTask.WhenAll(
            _enemyService.InitializeAsync(),
            _playerConfigService.InitializeAsync(),
            _levelService.InitializeAsync()
            );
        _signalBus.Subscribe<NextLevelSignal>(_levelService.NextLevel);
    }
}
