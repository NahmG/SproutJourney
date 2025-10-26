using System;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;


public class Level : MonoBehaviour
{
    [SerializeField]
    Transform spawnPoint;
    public Transform SpawnPoint => spawnPoint;

    [SerializeField]
    List<Sprout> sprouts = new();
    public List<Sprout> Sprouts => sprouts;

    [SerializeField]
    List<Land> lands = new();
    public List<Land> Lands => lands;

    void Awake()
    {
        foreach (var sprout in sprouts)
        {
            sprout._OnCollected += OnSproutCollected;
        }
    }

    void OnDestroy()
    {
        foreach (var sprout in sprouts)
        {
            sprout._OnCollected -= OnSproutCollected;
        }
    }

    public void OnInit()
    {
        foreach (var land in lands)
        {
            land.Init();
        }
    }

    public void OnSproutCollected(Sprout sprout)
    {
        sprouts.Remove(sprout);
        if (sprouts.Count == 0)
        {
            DOVirtual.DelayedCall(.5f, OnLevelComplete);
        }

        void OnLevelComplete()
        {
            GameplayManager.Ins.OnGameEnd(true);
        }
    }

    public void OnPlayerExitLand(Land oldLand)
    {
        if (oldLand == null) return;

        Debug.Log("Spawn New Land");

        int index = oldLand.Index;
        LAND_TYPE landType = oldLand.LandType;
        Vector3 pos = oldLand.transform.position;

        if (landType == LAND_TYPE.GRASS) return;
        oldLand.OnDespawn(() => SpawnNextLand(landType, index));


        //Helper functions
        void SpawnNextLand(LAND_TYPE landType, int index)
        {
            int nextIndex = NextLandIndex(landType, index);
            if (CanSpawnLand(landType, nextIndex))
            {
                Land newLand = Land.CreateLand(transform, landType, nextIndex);
                newLand.transform.position = pos;
            }
        }
        int NextLandIndex(LAND_TYPE landType, int currentIndex)
        {
            return landType switch
            {
                LAND_TYPE.SNOW => currentIndex - 1,
                LAND_TYPE.SAND => currentIndex + 1,
                LAND_TYPE.VOLCANIC => 7 - currentIndex,
                _ => currentIndex
            };
        }
        bool CanSpawnLand(LAND_TYPE landType, int index)
        {
            return landType switch
            {
                LAND_TYPE.GRASS => false,
                LAND_TYPE.SNOW => index >= 1,
                LAND_TYPE.SAND => index <= 6,
                LAND_TYPE.VOLCANIC => index >= 1 && index <= 6,
                _ => false
            };
        }
    }
}
