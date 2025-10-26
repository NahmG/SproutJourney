using UnityEngine;

public class UIWin : UICanvas
{
    [SerializeField]
    UIButton nextBtn;

    void Awake()
    {
        nextBtn._OnClick += OnNextBtnClick;
    }

    void OnDestroy()
    {
        nextBtn._OnClick -= OnNextBtnClick;
    }

    void OnNextBtnClick(int index)
    {
        if (LevelManager.Ins.NextLevel())
        {
            UIManager.Ins.OpenUI<UILoading>();
            Hide();
        }
        else
        {
            GameplayManager.Ins.DestructLevel();
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<UILevelSelect>();
        }
    }
}
