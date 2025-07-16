using System;

public interface ICoroutineManager
{
    void RunDelayedAction(float duration, Action action);
}