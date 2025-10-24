using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    private BaseState currentState;
    private Dictionary<STATE, BaseState> _states;
    public bool IsDebug;

    public STATE CurrentState => currentState.Id;
    public Dictionary<STATE, BaseState> States => _states;

    public StateMachine()
    {
        _states = new Dictionary<STATE, BaseState>();
    }

    public void Start(STATE id)
    {
        currentState = _states[id];
        currentState?.Enter();

        if (IsDebug)
        {
            Debug.Log($"Change state to {id}");
        }
    }

    public void Stop()
    {
        currentState?.Exit();
        currentState = null;
    }

    public void AddState(STATE id, BaseState state)
    {
        if (!_states.ContainsKey(id))
        {
            _states.Add(id, state);
            _states[id]._OnStateChanged += ChangeState;
        }
    }

    public void RemoveState(STATE id)
    {
        if (_states.ContainsKey(id))
        {
            _states[id]._OnStateChanged -= ChangeState;
            _states.Remove(id);
        }
    }

    public void ChangeState(STATE id)
    {
        if (IsDebug)
        {
            Debug.Log($"Change state from {currentState?.Id} to {id}");
        }

        if (!_states.ContainsKey(id))
        {
            Debug.Log($"Key {id} STATE Invalid");
        }
        currentState?.Exit();
        currentState = _states[id];
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}
