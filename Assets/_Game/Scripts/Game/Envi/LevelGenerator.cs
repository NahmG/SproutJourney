using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    Level newLevel;

    [SerializeField]
    Sprout sproutPrefab;
    [SerializeField]
    Land landPrefab;

#if UNITY_EDITOR
    [Button, PropertySpace(20)]
    void NewLevel()
    {
        GameObject g = new GameObject();
        g.name = "NewLevel";
        newLevel = g.AddComponent<Level>();
    }

    [Button, PropertySpace(5)]
    void AddSprout()
    {
        if (newLevel == null) return;

        Sprout sprout = Instantiate(sproutPrefab, newLevel.transform);
        newLevel.Sprouts.Add(sprout);
    }

    [Button, PropertySpace(10)]
    void SpawnLand(LAND_TYPE landType, [PropertyRange(1, 6)] int index = 1)
    {
        if (newLevel == null) return;
        Land.CreateLand(landPrefab, newLevel.transform, landType, index);
    }

    [Button, PropertySpace(20)]
    void SaveToPrefab()
    {
        if (newLevel == null) return;

        string folderPath = "Assets/_Game/Prefabs/Levels";
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Generate a prefab file name
        string prefabPath = $"{folderPath}/Level.prefab";

        // Save as prefab asset
        PrefabUtility.SaveAsPrefabAsset(newLevel.gameObject, prefabPath);

        Debug.Log($"✅ Saved prefab at: {prefabPath}");
    }
#endif
}