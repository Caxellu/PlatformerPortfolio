using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPresenter : IDisposable
{
    private readonly EnemyFactory _enemyFactory;
    private readonly IPlayerPos _playerPos;
    private readonly SignalBus _signalBus;

    private List<Action> _restartHandler= new List<Action>();
    private List<Action> _freezeHandler = new List<Action>();
    private List<Action> _unfreezeHandler = new List<Action>();
    public EnemyPresenter(EnemyFactory enemyfactory, IPlayerPos playerPos, SignalBus signalBus)
    {
        _enemyFactory = enemyfactory;
        _playerPos = playerPos;
        _signalBus = signalBus;
    }
    public void Intialize(List<EnemyDTO> data, Transform parent)
    {
        foreach (EnemyDTO enemyMove in data)
        {
            GameObject obj = new GameObject("PatrolPath");
            PatrolPath patrolPath = obj.AddComponent<PatrolPath>();
            patrolPath.transform.position = enemyMove.PatrolPath.globalPosition;
            patrolPath.startPosition = enemyMove.PatrolPath.startPosition;
            patrolPath.endPosition = enemyMove.PatrolPath.endPosition;

            EnemyModel model;
            IEnemyMovement movement;
            IEnemyView view = _enemyFactory.Spawn(enemyMove.Type, enemyMove.EnemySpawnPos, parent,out model, out movement);

            view.OnCauseDamage += model.CauseDamage;
            view.OnTakeDamageAction += model.TakeDamage;
            view.OnUpdateAction += ()=>model.CheckIsPlayerFound(view.Position);
            movement.OnDirectionAction += model.SetDirection;
            movement.OnDirectionAction += view.SetDirection;
            model.OnPlayerFound += movement.PlayerFound;
            model.Health.OnDie += ()=>view.SetActive(false);


            model.Initialize(_playerPos);
            movement.Initialize(model.Speed, patrolPath, _playerPos);


            Action restartAction = () => Respawn(movement, model, view);
            _restartHandler.Add(restartAction);

            Action freezeAction = () => movement.SetFreeze();
            _freezeHandler.Add(freezeAction);

            Action unfreezeAtion = () => movement.SetUnFreeze();
            _unfreezeHandler.Add(unfreezeAtion);

            _signalBus.Subscribe<RestartSignal>(restartAction);
            _signalBus.Subscribe<FreezeSignal>(freezeAction);
            _signalBus.Subscribe<UnFreezeSignal>(unfreezeAtion);
        }
    }
    public void Dispose()
    {
        foreach (Action action in _restartHandler)
        {
            _signalBus.Unsubscribe<RestartSignal>(action);
        }
        foreach (Action action in _freezeHandler)
        {
            _signalBus.Unsubscribe<FreezeSignal>(action);
        }
        foreach (Action action in _unfreezeHandler)
        {
            _signalBus.Unsubscribe<UnFreezeSignal>(action);
        }
    }
    private void Respawn(IEnemyMovement movement, EnemyModel model, IEnemyView view)
    {
        movement.SetPosition(model.SpawnPos);
        movement.PlayerLost();
        model.Reset();
        view.SetActive(true);
    }
}
