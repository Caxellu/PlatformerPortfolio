using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBullet : MonoBehaviour
{
    UnityAction<HitBullet> _returnToPoolAction;
    public void Initialize(float waitToReturn, UnityAction<HitBullet> action)
    {
        _returnToPoolAction = action;
        StartCoroutine(Wait(waitToReturn));
    }
    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        _returnToPoolAction?.Invoke(this);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
