using UnityEngine;

public class UISetting : UICanvas
{
    [SerializeField]
    UIButton homeBtn, continueBtn, replayBtn;

    void Awake()
    {
        homeBtn._OnClick += OnHomeBtnClick;
        continueBtn._OnClick += OnContinueBtnClick;
        replayBtn._OnClick += OnReplayBtnClick;
    }

    void OnDestroy()
    {
        homeBtn._OnClick -= OnHomeBtnClick;
        continueBtn._OnClick -= OnContinueBtnClick;
        replayBtn._OnClick -= OnReplayBtnClick;
    }

    void OnHomeBtnClick(int index)
    {
        GameplayManager.Ins.DestructLevel();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    void OnContinueBtnClick(int index)
    {
        Hide();
    }

    void OnReplayBtnClick(int index)
    {
        UIManager.Ins.OpenUI<UILoading>();
        Hide();
    }
}
