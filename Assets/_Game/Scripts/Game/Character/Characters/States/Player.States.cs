using Core;
using Core.Movement;
using Core.Navigation;
using UnityEngine;

public class PlayerIdleState : IdleState
{
    PlayerNavigation _nav;
    public PlayerIdleState(CoreSystem core) : base(core)
    {
        _nav = core.NAVIGATION as PlayerNavigation;
    }

    public override void Enter()
    {
        base.Enter();
        InputHandler.Ins.ResetInput();
    }

    public override void Update()
    {
        base.Update();

        if (_nav.MoveDirection.sqrMagnitude > .01f && _nav.CanMove(out _))
        {
            ChangeState(STATE.MOVE);
        }
    }
}

public class PlayerMoveState : MoveState
{
    PlayerNavigation _nav;
    PlayerMovement _move;
    Cell targetCell;
    public PlayerMoveState(CoreSystem core) : base(core)
    {
        _nav = core.NAVIGATION as PlayerNavigation;
        _move = core.MOVEMENT as PlayerMovement;
    }

    public override void Enter()
    {
        base.Enter();
        _nav.GetDestination(out targetCell);
    }

    public override void Update()
    {
        base.Update();
        if (_nav.ReachDestination())
        {
            ChangeState(STATE.IDLE);
            Player player = _char as Player;
            player.OnEnterNewCell(targetCell);
        }

        Core.DISPLAY.SetSkinRotation(Quaternion.LookRotation(_nav.MoveDirection), true);
        _move.MoveToDestination(_nav.Destination);
    }
}

public class PlayerInAirState : InAirState
{
    public PlayerInAirState(CoreSystem core) : base(core)
    {
    }
}

public class PlayerAttackState : AttackState
{
    public PlayerAttackState(CoreSystem core) : base(core)
    {
    }

    public override void Update()
    {
        if (Core.NAVIGATION.MoveDirection.sqrMagnitude > .01f)
        {
            ChangeState(STATE.MOVE);
        }
        base.Update();
    }

    protected override void RotateTowardTarget()
    {
        base.RotateTowardTarget();

        Vector3 dir = Core.SENSOR.TargetDir;
        Core.DISPLAY.SetSkinRotation(Quaternion.LookRotation(dir), true);
    }
}

public class PlayerDeadState : DeadState
{
    public PlayerDeadState(CoreSystem core) : base(core)
    {
    }

    public override void OnDeath()
    {
        base.OnDeath();

    }
}

public class PlayerWinState : BaseLogicState
{
    public override STATE Id => STATE.WIN;
    public PlayerWinState(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {

    }
}
