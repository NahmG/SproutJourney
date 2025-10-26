using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    public Level[] levels = new Level[15];

    [HideInInspector]
    public Level currentLevel;

    LevelManagerData _data;
    public LevelManagerData Data
    {
        get
        {
            _data ??= DataManager.Ins.GameData.levels;
            return _data;
        }
    }
    public int LevelIndex => Data.currentLevelIndex;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public bool NextLevel()
    {
        int next = LevelIndex + 1;
        LevelData level = DataManager.Ins.GameData.GetLevel(next);

        if (level != null && level.state != LevelData.LevelState.LOCK)
        {
            DataManager.Ins.GameData.SetCurrentLevel(LevelIndex + 1);
            return true;
        }
        return false;
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
        return LevelIndex > 1 ? levels[LevelIndex - 1] : levels[0];
    }

    public void OnPlayerExitLand(Land oldLand)
    {
        currentLevel?.OnPlayerExitLand(oldLand);
    }
}