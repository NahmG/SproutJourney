using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "LandData", menuName = "ScriptableObject/LandData", order = 1)]
public class LandData : SerializedScriptableObject
{
    public List<GameObject> lands;
    public List<GameObject> obstacles;
    public List<int[,]> patterns = new()
    {
        new int[3, 3],
        new int[3, 3],
        new int[3, 3],
        new int[3, 3],
        new int[3, 3],
        new int[3, 3]
    };

    public GameObject GetLandPrefab(int landType)
    {
        return lands[landType];
    }

    public GameObject GetObstaclePrefab(int landType)
    {
        return obstacles[landType];
    }

    public int[,] GetPattern(int index)
    {
        return patterns[index];
    }
}