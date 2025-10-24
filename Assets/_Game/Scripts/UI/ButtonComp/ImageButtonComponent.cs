using UnityEngine;
using UnityEngine.UI;

public class ImageButtonComponent : UIButtonComponent
{
    [SerializeField]
    ImageState[] Data;

    public override void SetState(UIButton.STATE state)
    {
        for (int i = 0; i < Data.Length; i++)
        {
            if (Data[i].Sprites.Length > (int)state)
            {
                Data[i].Image.sprite = Data[i].Sprites[(int)state];
            }
            if (Data[i].RaycaseTargets.Length > (int)state)
            {
                Data[i].Image.raycastTarget = Data[i].RaycaseTargets[(int)state];
            }
            if (Data[i].Colors.Length > (int)state)
            {
                Data[i].Image.color = Data[i].Colors[(int)state];
            }
            if (Data[i].Types.Length > (int)state)
            {
                Data[i].Image.type = Data[i].Types[(int)state];
            }
            if (Data[i].PreserveAspects.Length > (int)state)
            {
                Data[i].Image.preserveAspect = Data[i].PreserveAspects[(int)state];
            }
            if (Data[i].Positions.Length > (int)state)
            {
                Data[i].Image.rectTransform.anchoredPosition = Data[i].Positions[(int)state];
            }
            if (Data[i].Sizes.Length > (int)state)
            {
                Data[i].Image.rectTransform.sizeDelta = Data[i].Sizes[(int)state];
            }
        }
    }

    public Image GetImage(int index = 0)
    {
        if (index < 0 || index >= Data.Length)
            return null;
        return Data[index].Image;
    }
}