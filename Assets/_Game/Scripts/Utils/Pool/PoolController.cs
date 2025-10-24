using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PoolControler : MonoBehaviour
{
    [Space]
    [Header("Pool")]
    public List<GameUnit> PoolNoneRoot;

    [Header("Pool")]
    public List<PoolAmount> PoolWithRoot;

    [Header("Particle")]
    public ParticleAmount[] Particle;


    public void Awake()
    {
        for (int i = 0; i < PoolNoneRoot.Count; i++)
        {
            SimplePool.Preload(PoolNoneRoot[i], 0, transform);
        }

        for (int i = 0; i < PoolWithRoot.Count; i++)
        {
            SimplePool.Preload(PoolWithRoot[i].prefab, PoolWithRoot[i].amount, PoolWithRoot[i].root);
        }

        for (int i = 0; i < Particle.Length; i++)
        {
            ParticlePool.Preload(Particle[i].prefab, Particle[i].amount, Particle[i].root);
            ParticlePool.Shortcut(Particle[i].particleType, Particle[i].prefab);
        }
    }
}

[System.Serializable]
public class PoolAmount
{
    [Header("-- Pool Amount --")]
    public Transform root;
    public GameUnit prefab;
    public int amount;

    public PoolAmount(Transform root, GameUnit prefab, int amount)
    {
        this.root = root;
        this.prefab = prefab;
        this.amount = amount;
    }
}


[System.Serializable]
public class ParticleAmount
{
    public Transform root;
    public ParticleType particleType;
    public ParticleSystem prefab;
    public int amount;
}


public enum ParticleType
{
    BLOOD_EXPLOSION_ROUND = 0,
    SINGLE_THUNDER = 10,
}

public enum PoolType
{
    NONE = 0,
    AUDIO = 1,
    PLAYER = 2,
    ENEMY = 3,
    LAND = 4,
    BULLET = 100,
}