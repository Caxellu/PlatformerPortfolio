using System.Collections;
using UnityEngine;
using System;

public class CoroutineManager : MonoBehaviour, ICoroutineManager
{
    public void RunDelayedAction(float duration, Action action)
    {
        StartCoroutine(WaitAndInvoke(duration, action));
    }

    private IEnumerator WaitAndInvoke(float duration, Action action)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
}
