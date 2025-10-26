using System.Collections.Generic;
using UnityEngine;

public class UILevelSelect : UICanvas
{
    [SerializeField]
    UIButton homeBtn, levelBtnPref;

    [SerializeField]
    RectTransform grid;

    List<UIButton> levelBtns = new();

    LevelData[] levels;

    void Awake()
    {
        CreateLevelBtn();
        foreach (var button in levelBtns)
        {
            button._OnClick += OnLevelBtnClick;
        }
        homeBtn._OnClick += OnHomeBtnClick;
    }

    void OnDestroy()
    {
        foreach (var button in levelBtns)
        {
            button._OnClick -= OnLevelBtnClick;
        }
        homeBtn._OnClick -= OnHomeBtnClick;
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        UpdateBtn();
    }

    void OnLevelBtnClick(int index)
    {
        LevelManager.Ins.SetLevel(index);
        GameplayManager.Ins.LoadGame();
        GameplayManager.Ins.StartLevel();

        UIManager.Ins.OpenUI<UIGameplay>();
        Close();
    }

    void OnHomeBtnClick(int index)
    {
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close();
    }

    void CreateLevelBtn()
    {
        levels = LevelManager.Ins.levelDatas;
        for (int i = 0; i < levels.Length; i++)
        {
            UIButton btn = Instantiate(levelBtnPref, grid);
            btn.SetIndex(i);
            btn.SetData(levels[i].index.ToString());
            btn.SetState((int)levels[i].state);

            levelBtns.Add(btn);
        }
    }

    void UpdateBtn()
    {
        foreach (var button in levelBtns)
        {
            int index = levelBtns.IndexOf(button);
            button.SetState((int)levels[index].state);
        }
    }
}
