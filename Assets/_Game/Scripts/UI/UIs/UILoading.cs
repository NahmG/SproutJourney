using System;
using DG.Tweening;
using UnityEngine;

public class UILoading : UICanvas
{
    [SerializeField]
    float loadingTime;

    [SerializeField]
    RectTransform maskTF;

    int levelIndex;

    public override void Open(object param = null)
    {
        base.Open(param);

        if (param != null && param is int v)
        {
            levelIndex = v;
            DataManager.Ins.GameData.SetCurrentLevel(levelIndex);
        }

        AnimLoading();
    }

    void AnimLoading()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(maskTF.DOScale(0, loadingTime).OnComplete(() =>
        {
            GameplayManager.Ins.LoadLevel();
            UIManager.Ins.OpenUI<UIGameplay>();
        }));
        seq.AppendInterval(.5f);
        seq.Append(maskTF.DOScale(1, loadingTime)).OnComplete(() =>
        {
            Close();
        });

        seq.SetAutoKill(true);
    }
}