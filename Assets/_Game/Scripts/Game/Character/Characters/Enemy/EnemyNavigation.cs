using UnityEngine;
using UnityEngine.AI;

namespace Core.Navigation
{
    public class EnemyNavigation : NavigationCore
    {
        [SerializeField]
        Enemy _enemy;
        [SerializeField]
        NavMeshAgent agent;
        bool isRunning = false;

        override public void UpdateData()
        {
            if (!isRunning) return;

            Vector3 dir = agent.velocity.normalized;
            dir.y = 0;

            MoveDirection = dir;
        }

        public override void StartNavigation()
        {
            isRunning = true;
            agent.enabled = true;
        }

        public override void StopNavigation()
        {
            isRunning = false;
            agent.enabled = false;
        }

        public override bool ReachDestination()
        {
            return agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending;
        }
    }

}