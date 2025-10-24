using System.Collections.Generic;
using UnityEngine;

public static class UTILS
{
    public static List<T> Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    public static bool IsPlatformValid(Vector3 start, Vector3 dir, LayerMask layer, out Cell cell)
    {
        cell = null;
        Vector3 origin = start + dir;

        if (Physics.Raycast(origin + Vector3.up * 5f, Vector3.down, out RaycastHit hit, 10f, layer))
        {
            cell = hit.collider.GetComponent<Cell>();
            return cell != null && cell.IsEmpty;
        }
        return false;
    }
}