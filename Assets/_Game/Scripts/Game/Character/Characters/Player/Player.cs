using System;
using Core.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class Player : Character
{
    public Action<Land> _OnExitLand;
    PlayerNavigation _nav;

    public override void OnInit(CharacterStats stats = null)
    {
        base.OnInit(stats);
        _nav = Core.NAVIGATION as PlayerNavigation;
    }

    public void SetCell(Cell cell)
    {
        _nav.currentCell = cell;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        StartNavigation(false);
        Core.isInit = false;
    }

    public override void OnDeath()
    {
        base.OnDeath();

        StartNavigation(false);
        Core.MOVEMENT.StopMovement();
        Core.DISPLAY.SetSkinRotation(Quaternion.identity, true);
    }

    public void OnEnterNewCell(Cell cell)
    {
        if (_nav.currentCell != null && cell.Land != _nav.currentCell.Land)
            _OnExitLand?.Invoke(_nav.currentCell.Land);

        _nav.currentCell = cell;
    }
}
