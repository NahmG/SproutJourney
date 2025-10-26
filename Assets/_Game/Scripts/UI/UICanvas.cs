using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class UICanvas : MonoBehaviour
{
    //public bool IsAvoidBackKey = false;
    [FormerlySerializedAs("IsDestroyOnClose")]
    public bool isDestroyOnClose;

    [Header("Anim")]
    [SerializeField]
    protected bool useAnimator;
    [SerializeField]
    protected bool useFadeAnim;
    [ShowIf("useAnimator")]
    [SerializeField]
    Animator anim;
    string currentAnim = "Open";

    [ShowIf("useFadeAnim")]
    [SerializeField]
    CanvasGroup canvasGroup;

    [Header("Ref")]
    [SerializeField]
    private RectTransform rectTf;
    public RectTransform RectTf => rectTf;

    private Canvas _canvas;
    public Canvas Canvas
    {
        get
        {
            _canvas = _canvas ? _canvas : GetComponent<Canvas>();
            return _canvas;
        }
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        if (useFadeAnim)
        {
            RunFadeAnimation(.3f, true);
        }
    }

    public virtual void Hide()
    {
        if (useFadeAnim)
        {
            RunFadeAnimation(.3f, false, () => gameObject.SetActive(false));
        }
        else gameObject.SetActive(false); ;
    }

    public virtual void Setup(object param = null)
    {
        UIManager.Ins.AddBackUI(this);
        UIManager.Ins.PushBackAction(this, BackKey);
    }

    protected virtual void BackKey()
    {

    }

    public virtual void Open(object param = null)
    {
        UpdateUI();
        gameObject.SetActive(true);

        if (useFadeAnim)
        {
            RunFadeAnimation(.2f, true);
        }
    }

    public virtual void UpdateUI() { }

    public virtual void Close()
    {
        UIManager.Ins.RemoveBackUI(this);
        if (useFadeAnim)
        {
            RunFadeAnimation(.3f, false, OnClose);
        }
        else OnClose();
    }

    public virtual void CloseDirectly(object param = null)
    {
        if (UIManager.Ins.IsContain(this))
        {
            UIManager.Ins.RemoveBackUI(this);
        }
        else OnClose();
    }

    private void OnClose()
    {
        gameObject.SetActive(false);
        if (isDestroyOnClose) Destroy(gameObject);
    }

    public void ChangeAnim(string animName)
    {
        if (animName != currentAnim)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    void RunFadeAnimation(float time, bool open, Action action = null)
    {
        float from = open ? 0 : 1;
        float to = open ? 1 : 0;

        Sequence seq = DOTween.Sequence();

        seq.Append(canvasGroup.DOFade(to, time).From(from));
        seq.Append(rectTf.DOScale(to, time).From(from));

        seq.SetEase(Ease.OutQuad);

        seq.OnComplete(() => action?.Invoke());
    }
}
