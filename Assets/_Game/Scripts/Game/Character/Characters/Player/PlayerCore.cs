using UnityEngine;

using Core;
public class PlayerCore : CoreSystem
{
    public override void Initialize(CharacterStats stats)
    {
        base.Initialize(stats);

        StateMachine.IsDebug = true;

        StateMachine.AddState(STATE.IDLE, new PlayerIdleState(this));
        StateMachine.AddState(STATE.MOVE, new PlayerMoveState(this));
        StateMachine.AddState(STATE.ATTACK, new PlayerAttackState(this));
        StateMachine.AddState(STATE.DEAD, new PlayerDeadState(this));
        StateMachine.AddState(STATE.WIN, new PlayerWinState(this));

        StateMachine.Start(STATE.IDLE);
    }

    public override void Run()
    {
        base.Run();
        StateMachine.Start(STATE.IDLE);
    }
}