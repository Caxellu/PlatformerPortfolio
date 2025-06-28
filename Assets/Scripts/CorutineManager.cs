using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CorutineManager : MonoBehaviour
{
    public void WaitAndActionCorutineCall(float duration, UnityAction action)
    {
        StartCoroutine(WaitAndAction(duration, action));
    }
   IEnumerator WaitAndAction(float duration, UnityAction action)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }
}
