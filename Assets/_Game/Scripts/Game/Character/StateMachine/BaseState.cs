using System;
using UnityEngine;

public enum STATE
{
    NONE,
    IDLE,
    MOVE,
    IN_AIR,
    TARGET_DETECTED,
    ATTACK,
    ULTI,
    DEAD,
    WIN,
    SHOP_SKIN,
}

[Serializable]
public abstract class BaseState
{
    public event Action<STATE> _OnStateChanged;
    public abstract STATE Id { get; }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
    public virtual void UnregisterEvent() { }
    protected void ChangeState(STATE newState)
    {
        _OnStateChanged?.Invoke(newState);
    }

}

