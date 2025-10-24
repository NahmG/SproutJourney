using UnityEngine;

using Core;
using System.Collections;
using System.Collections.Generic;
public class Character : GameUnit, ICharacter
{
    #region CORE
    public CharacterStats Stats;
    [SerializeField]
    CoreSystem core;
    public CoreSystem Core => core;
    public bool IsDead => Stats.HP.Value <= 0;

    protected virtual void Awake()
    {
        CharacterStats newStats = ScriptableObject.CreateInstance<CharacterStats>();
        newStats.OnInit(Stats);
        Stats = newStats;
    }

    public virtual void OnInit(CharacterStats stats = null)
    {
        if (stats == null)
            Stats.Reset();
        else
            Stats = stats;

        core.Initialize(Stats);
    }

    public virtual void Run()
    {
        StartNavigation(true);
        Core.Run();
    }

    public virtual void OnDespawn()
    {
    }

    public void OnHit(int dmg)
    {
        Stats.HP.Plus(-dmg);
        if (Stats.HP.Value <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        core.OnDeath();
    }

    protected virtual void Update()
    {
        core.UpdateData();
    }

    protected virtual void FixedUpdate()
    {
        core.FixedUpdate();
    }

    public void StartNavigation(bool state)
    {
        if (state)
        {
            Core.NAVIGATION.StartNavigation();
        }
        else
        {
            Core.NAVIGATION.StopNavigation();
        }
    }

    public virtual void ChangeState(STATE state)
    {
        Core.StateMachine.ChangeState(state);
    }

    #endregion
}
