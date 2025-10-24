using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    Cell playerSpawnPoint;
    public Cell PlayerSpawnPoint => playerSpawnPoint;

    [SerializeField]
    List<Sprout> sprouts = new();
    public List<Sprout> Sprouts => sprouts;

    public void OnInit()
    {
        foreach (var sprout in sprouts)
        {
            sprout._OnCollected += OnSproutCollected;
        }
    }

    public void OnSproutCollected(Sprout sprout)
    {
        sprouts.Remove(sprout);
        if (sprouts.Count == 0)
        {
            GameplayManager.Ins.OnGameEnd(true);
        }
    }

    public void OnPlayerExitLand(Land oldLand)
    {
        if (oldLand != null)
        {
            oldLand.SpawnNextLand();
        }
    }
}
