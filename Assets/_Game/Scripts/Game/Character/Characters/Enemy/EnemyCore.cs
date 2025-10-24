using UnityEngine;

namespace Core
{
    public class EnemyCore : CoreSystem
    {
        public override void Initialize(CharacterStats stats)
        {
            base.Initialize(stats);

            // StateMachine.IsDebug = true;

            StateMachine.AddState(STATE.IDLE, new EnemyIdleState(this));
            StateMachine.AddState(STATE.MOVE, new EnemyMoveState(this));
            StateMachine.AddState(STATE.ATTACK, new EnemyAttackState(this));
            StateMachine.AddState(STATE.DEAD, new EnemyDeadState(this));
        }

        public override void Run()
        {
            base.Run();
            StateMachine.Start(STATE.IDLE);
        }
    }
}