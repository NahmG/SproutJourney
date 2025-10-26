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
        Land land = Land.CreateLand(landPrefab, newLevel.transform, landType, index);
        newLevel.Lands.Add(land);
    }

    [Button, PropertySpace(20)]
    void SaveToPrefab()
    {
        if (newLevel == null) return;

        string folderPath = "Assets/_Game/Prefabs/Levels";
        string baseName = "Level";
        string prefabName = baseName;
        string prefabPath = $"{folderPath}/{prefabName}.prefab";

        // Create folder if needed
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Debug.LogWarning($"Path {folderPath} Invalid!");
            return;
        }

        // Find available name
        int counter = 0;
        while (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
        {
            prefabName = $"{baseName}_{counter}";
            prefabPath = $"{folderPath}/{prefabName}.prefab";
            counter++;
        }

        // Save the prefab
        PrefabUtility.SaveAsPrefabAssetAndConnect(newLevel.gameObject, prefabPath, InteractionMode.UserAction);
        Debug.Log($"Prefab saved as: {prefabName}");
    }
#endif
}