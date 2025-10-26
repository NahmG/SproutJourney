using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField]
    UIButton[] moveBtns;

    [SerializeField]
    UIButton settingBtn;

    [SerializeField]
    TMP_Text levelTxt;


    void Awake()
    {
        moveBtns.ForEach(btn =>
        {
            btn._OnPointerHold += OnMoveBtnHold;
        });
        settingBtn._OnClick += OnSettingBtnClick;
    }

    void OnDestroy()
    {
        moveBtns.ForEach(btn =>
        {
            btn._OnPointerHold -= OnMoveBtnHold;
        });
        settingBtn._OnClick -= OnSettingBtnClick;
    }

    public override void Open(object param = null)
    {
        base.Open(param);
        levelTxt.text = $"Level {LevelManager.Ins.LevelIndex}";
    }

    void OnMoveBtnHold(int index)
    {
        InputHandler.Ins.SetMoveInput(index);
    }

    void OnSettingBtnClick(int index)
    {
        UIManager.Ins.OpenUI<UISetting>();
    }
}
