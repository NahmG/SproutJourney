using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Movement
{
    public class EnemyMovement : MovementCore
    {
        [SerializeField]
        NavMeshAgent agent;

        public override void Initialize(CoreSystem core)
        {
            base.Initialize(core);
            agent.speed = core.Stats.Speed.Value;
        }

        public override void UpdateData()
        {
            base.UpdateData();
        }

        public override void SetVelocity(Vector3 velocity)
        {
            base.SetVelocity(velocity);
            agent.velocity = velocity;
        }

        public override void MoveToDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public override void StopMovement()
        {
            base.StopMovement();

            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }
    }
}