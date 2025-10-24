using Sirenix.Utilities;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField]
    UIButton[] moveBtns;

    void Awake()
    {
        moveBtns.ForEach(btn =>
        {
            btn._OnPointerHold += OnMoveBtnHold;
        });
    }

    public void OnMoveBtnHold(int index)
    {
        InputHandler.Ins.SetMoveInput(index);
    }
}
