using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    private Transform tf;
    public Transform TF
    {
        get
        {
            //tf = tf ?? gameObject.transform;
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }
    [SerializeField]
    private Transform skinTF;
    public Transform SkinTF => skinTF;

    public PoolType poolType;

    public void OnDespawn(float delay)
    {
        Invoke(nameof(OnDespawn), delay);
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

}