using System;
using UnityEngine;

public interface ICharacter
{
    public Transform TF { get; }
    public Transform SkinTF { get; }
    public bool IsDead { get; }
    public void OnInit(CharacterStats stats);
}