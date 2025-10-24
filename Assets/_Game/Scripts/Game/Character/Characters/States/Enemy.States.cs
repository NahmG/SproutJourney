
using Core;
using Core.Movement;
using Core.Navigation;
using UnityEngine;

public class EnemyIdleState : IdleState
{
    float idleTime;
    float timer;

    public EnemyIdleState(CoreSystem core) : base(core)
    {
    }

    public override void Enter()
    {
        base.Enter();
        idleTime = GetRandomTime(2, 3);
        timer = Time.time;

        Core.MOVEMENT.StopMovement();
    }

    public override void Update()
    {
        base.Update();
    }
}

public class EnemyMoveState : MoveState
{
    EnemyNavigation _nav;
    EnemyMovement _move;

    public EnemyMoveState(CoreSystem core) : base(core)
    {
        _nav = (EnemyNavigation)Core.NAVIGATION;
        _move = (EnemyMovement)Core.MOVEMENT;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (Target != null && !IsAttackCooldown)
        {
            ChangeState(STATE.TARGET_DETECTED);
        }

        if (_nav.ReachDestination())
        {
            ChangeState(STATE.IDLE);
        }
    }
}

public class EnemyAttackState : AttackState
{
    Enemy _enemy;
    public EnemyAttackState(CoreSystem core) : base(core)
    {
        _enemy = (Enemy)Core.CHARACTER;
    }

    protected override void RotateTowardTarget()
    {
        Vector3 dir = Core.SENSOR.TargetDir;
        _enemy.TF.rotation = Quaternion.LookRotation(dir);
    }
}

public class EnemyDeadState : DeadState
{
    public EnemyDeadState(CoreSystem core) : base(core)
    {
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }
}

