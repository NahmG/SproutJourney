using System;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public Action _OnActionStart;
    public Action _OnActionExecute;
    public Action _OnActionEnd;

    public void OnActionStart()
    {
        _OnActionStart?.Invoke();
    }

    public void OnActionExecute()
    {
        _OnActionExecute?.Invoke();
    }

    public void OnActionEnd()
    {
        _OnActionEnd?.Invoke();
    }
}
