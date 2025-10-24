using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Enemy : Character
{
    public override void OnInit(CharacterStats stats = null)
    {
        base.OnInit(stats);
        StartNavigation(false);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        StartNavigation(false);
    }
}
