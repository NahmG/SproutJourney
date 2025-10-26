using UnityEngine;

public class UILose : UICanvas
{
    [SerializeField]
    UIButton home, replay;

    void Awake()
    {
        home._OnClick += OnHomeBtnClick;
        replay._OnClick += OnReplayBtnClick;
    }

    void OnDestroy()
    {
        home._OnClick -= OnHomeBtnClick;
        replay._OnClick -= OnReplayBtnClick;
    }

    void OnReplayBtnClick(int index)
    {
        UIManager.Ins.OpenUI<UILoading>();
        Hide();
    }

    void OnHomeBtnClick(int index)
    {
        GameplayManager.Ins.DestructLevel();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<UIMainMenu>();
    }
}
