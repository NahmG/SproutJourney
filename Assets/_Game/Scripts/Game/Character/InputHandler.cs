using UnityEngine;

public class InputHandler : Singleton<InputHandler>
{
    enum Dir
    {
        None = -1,
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 3,
    }
    Dir moveInput = Dir.None;

    public void SetMoveInput(int index)
    {
        moveInput = (Dir)index;
    }

    public void ResetInput()
    {
        moveInput = Dir.None;
    }

    public Vector3 GetMoveDirection()
    {
        switch (moveInput)
        {
            case Dir.Left:
                return Vector3.left;
            case Dir.Right:
                return Vector3.right;
            case Dir.Up:
                return Vector3.forward;
            case Dir.Down:
                return Vector3.back;
            case Dir.None:
            default:
                return Vector3.zero;
        }
    }
}
