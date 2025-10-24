using UnityEngine;

namespace Core.Navigation
{
    public abstract class NavigationCore : BaseCore
    {
        public Vector3 MoveDirection { get; internal set; }
        public Vector3 Destination { get; internal set; }

        public override void Initialize(CoreSystem core)
        {
            base.Initialize(core);

            MoveDirection = Vector3.zero;
        }

        public virtual bool ReachDestination() { return false; }
        public virtual void StartNavigation() { }
        public virtual void StopNavigation() { }
    }
}
