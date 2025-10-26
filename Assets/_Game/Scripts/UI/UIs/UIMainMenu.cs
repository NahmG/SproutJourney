using UnityEngine;

public class UIMainMenu : UICanvas
{
    [SerializeField]
    UIButton playBtn;

    void Awake()
    {
        playBtn._OnClick += OnPlayBtnClick;
    }

    void OnDestroy()
    {
        playBtn._OnClick -= OnPlayBtnClick;
    }

    void OnPlayBtnClick(int index)
    {
        UIManager.Ins.OpenUI<UILevelSelect>();
        Close();
    }
}
