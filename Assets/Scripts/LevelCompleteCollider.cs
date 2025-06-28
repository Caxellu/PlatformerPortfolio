using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider2D))]
public class LevelCompleteCollider : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    void OnCollisionEnter2D(Collision2D collision)
    {
        _signalBus.Fire<LevelCompleteSignal>();
    }
}
