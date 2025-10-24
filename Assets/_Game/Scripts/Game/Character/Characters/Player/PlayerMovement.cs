using UnityEngine;

namespace Core.Movement
{
    public class PlayerMovement : MovementCore
    {
        [SerializeField]
        Transform TF;
        Vector3 velocity = Vector3.zero;

        CoreSystem core;

        public override void Initialize(CoreSystem core)
        {
            base.Initialize(core);
            this.core = core;
        }

        public override void MoveToDestination(Vector3 destination)
        {
            Vector3 current = TF.position;
            TF.position = Vector3.MoveTowards(current, destination, Time.deltaTime * core.Stats.Speed.Value);
        }

        public override void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }

        public override void StopMovement()
        {
            velocity = Vector3.zero;
        }
    }
}
