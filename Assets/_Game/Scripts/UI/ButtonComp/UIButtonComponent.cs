using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[RequireComponent(typeof(UIButton))]
public abstract class UIButtonComponent : MonoBehaviour
{
    [Serializable]
    protected class ImageState
    {
        public Image Image;
        [FoldoutGroup("Setting")] public Sprite[] Sprites;
        [FoldoutGroup("Setting")] public bool[] RaycaseTargets = new bool[] { false, true, true, false };
        [FoldoutGroup("Setting")] public Color[] Colors = new Color[] { new Color(1 / 3f, 1 / 3f, 1 / 3f), Color.white, Color.white, Color.white };
        [FoldoutGroup("Setting")] public Image.Type[] Types = new Image.Type[] { Image.Type.Sliced, Image.Type.Sliced, Image.Type.Sliced, Image.Type.Simple };
        [FoldoutGroup("Setting")] public bool[] PreserveAspects = new bool[] { false, false, false, true };
        [FoldoutGroup("Setting")] public Vector2[] Positions;
        [FoldoutGroup("Setting")] public Vector2[] Sizes;
    }
    [Serializable]
    protected class TextState
    {
        public TMP_Text Text;
        [FoldoutGroup("Setting")] public string[] Contents;
        [FoldoutGroup("Setting")] public bool[] Actives;
        [FoldoutGroup("Setting")] public Color[] Colors;
        [FoldoutGroup("Setting")] public int[] Sizes;
    }
    public abstract void SetState(UIButton.STATE state);
}
