using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HitView))]
public class HitBulletLifeCycle : MonoBehaviour
{
    private float _lifetime;
    private HitView _view;
    private void Awake()
    {
        _view = GetComponent<HitView>();
    }
    public void Construct(float lifetime)
    {
        _lifetime = lifetime;
    }

    public void StartLife()
    {
        _view.Activate();
        StartCoroutine(LifetimeRoutine());
    }

    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(_lifetime);
        _view.Deactivate();
    }
}
