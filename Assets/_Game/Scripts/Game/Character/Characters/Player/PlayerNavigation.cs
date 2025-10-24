using UnityEngine;

namespace Core.Navigation
{
    public class PlayerNavigation : NavigationCore
    {
        bool isRunning;
        CoreSystem core;

        public Cell currentCell;

        public override void Initialize(CoreSystem core)
        {
            base.Initialize(core);
            this.core = core;
        }

        public override void UpdateData()
        {
            if (!isRunning) return;
            MoveDirection = InputHandler.Ins.GetMoveDirection();
        }

        public void GetDestination(out Cell cell)
        {
            if (CanMove(out cell))
            {
                Destination = cell.Tf.position;
            }
            else
                Destination = core.CHARACTER.TF.position;
        }

        public override bool ReachDestination()
        {
            float distance = Vector3.Distance(core.CHARACTER.TF.position, Destination);
            return distance < 0.01f;
        }

        public bool CanMove(out Cell cell)
        {
            bool canMove = UTILS.IsPlatformValid(currentCell.Tf.position, MoveDirection, LayerMask.GetMask(CONSTANTS.CELL_LAYER), out cell);
            return canMove;
        }

        public override void StartNavigation()
        {
            isRunning = true;
        }

        public override void StopNavigation()
        {
            isRunning = false;
        }
    }
}
