using UnityEngine;

public class TextButtonComponent : UIButtonComponent
{
    [SerializeField]
    TextState[] Data;

    public override void SetState(UIButton.STATE state)
    {
        for (int i = 0; i < Data.Length; i++)
        {
            if (Data[i].Contents.Length > (int)state)
            {
                Data[i].Text.text = Data[i].Contents[(int)state];
            }
            if (Data[i].Actives.Length > (int)state)
            {
                Data[i].Text.gameObject.SetActive(Data[i].Actives[(int)state]);
            }
            if (Data[i].Colors.Length > (int)state)
            {
                Data[i].Text.color = Data[i].Colors[(int)state];
            }
            if (Data[i].Sizes.Length > (int)state)
            {
                Data[i].Text.fontSize = Data[i].Sizes[(int)state];
            }
        }
    }
}