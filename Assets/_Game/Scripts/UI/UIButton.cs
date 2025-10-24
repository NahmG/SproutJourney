using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action<int> _OnClick;
    public Action<int> _OnPointerDown;
    public Action<int> _OnPointerUp;
    public Action<int> _OnPointerHold;

    public enum STATE
    {
        DISABLE = 0,
        OPENING = 1,
        SELECTING = 2,
        CLOSING = 3,
    }

    [SerializeField] Button button;
    [SerializeField] protected int index;
    [SerializeField] bool hasComponent;

    [ShowIfGroup("Extra", Condition = "hasComponent")]
    [ShowIfGroup("Extra")]
    [SerializeField] TMP_Text textButton;
    [ShowIfGroup("Extra")]
    [SerializeField] protected UIButtonComponent[] buttonComponents;

    STATE state;
    public STATE State => state;

    bool isHeld = false;
    public bool IsHeld => isHeld;

    void Awake()
    {
        button.onClick.AddListener(OnBtnClick);
    }

    void Update()
    {
        if (isHeld)
        {
            _OnPointerHold?.Invoke(index);
        }
    }

    void OnDestroy()
    {
        button.onClick.RemoveListener(OnBtnClick);
    }

    public void SetData(string text)
    {
        if (textButton != null)
            textButton.text = text;
    }

    public void SetState(STATE state)
    {
        this.state = state;
        for (int i = 0; i < buttonComponents.Length; i++)
        {
            buttonComponents[i].SetState(state);
        }
    }

    public void SetState(int state)
    {
        this.state = (STATE)state;
        for (int i = 0; i < buttonComponents.Length; i++)
        {
            buttonComponents[i].SetState(this.state);
        }
    }

    public void SetInteractive(bool state)
    {
        button.interactable = state;
    }

    void OnBtnClick()
    {
        // AudioManager.Ins.PlaySFXClip(SFX_TYPE.CLICK, transform, 1);
        _OnClick?.Invoke(index);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        _OnPointerDown?.Invoke(index);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        _OnPointerUp?.Invoke(index);
    }
}

