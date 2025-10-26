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
        GameData gameData = DataManager.Ins.GameData;
        LevelData level = gameData.GetLevel(index);

        if (level.state != LevelData.LevelState.LOCK)
        {
            UIManager.Ins.OpenUI<UILoading>(level);
            Close();
        }
    }

    void OnHomeBtnClick(int index)
    {
        UIManager.Ins.OpenUI<UIMainMenu>();
        Close();
    }

    void CreateLevelBtn()
    {
        levels = DataManager.Ins.GameData.levels.levelDatas;

        for (int i = 0; i < levels.Length; i++)
        {
            UIButton btn = Instantiate(levelBtnPref, grid);
            btn.SetIndex(levels[i].index);
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
