using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levels = new Level[15];
    public LevelData[] levelDatas = new LevelData[15];

    [HideInInspector]
    public Level currentLevel;
    int levelIndex;
    public int Level => levelIndex;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void OnInit()
    {
        levelIndex = 1;
    }

    public void SetLevel(int index)
    {
        levelIndex = index;
    }

    public void NextLevel()
    {
        levelIndex = levelIndex < levels.Length ? levelIndex + 1 : levelIndex;
    }

    public Level LoadLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(GetPrefabLevel(), transform);
        return currentLevel;
    }

    Level GetPrefabLevel()
    {
        return levelIndex > 1 ? levels[levelIndex - 1] : levels[0];
    }

    public void LockLevel(int id) => levelDatas[id - 1].state = LevelData.LevelState.LOCK;
    public void UnlockLevel(int id) => levelDatas[id - 1].state = LevelData.LevelState.PLAY;
    //complete current level
    public void CompletedLevel() => levelDatas[Level - 1].state = LevelData.LevelState.COMPLETE;

    public void OnPlayerExitLand(Land oldLand)
    {
        currentLevel?.OnPlayerExitLand(oldLand);
    }
}