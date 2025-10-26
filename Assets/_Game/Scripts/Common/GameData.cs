using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public static class Database
{
    public static void Save<T>(T data) where T : new()
    {
        string dataString = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString(typeof(T).Name, dataString);
        PlayerPrefs.Save();
    }

    public static T Load<T>() where T : new()
    {
        string key = typeof(T).Name;
        if (PlayerPrefs.HasKey(key))
        {
            return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key));
        }
        T data = new();
        Save(data);
        return data;
    }
}

public class GameData
{
    public LevelManagerData levels = new();

    public void InitData()
    {
        levels.levelDatas = new LevelData[15];
        for (int i = 0; i < 15; i++)
        {
            levels.levelDatas[i] = new LevelData(i + 1);
        }

        for (int i = 1; i <= 5; i++)
        {
            UnlockLevel(i);
        }
    }

    public void SetCurrentLevel(int index) => levels.currentLevelIndex = index;
    public void LockLevel(int id) => GetLevel(id).state = LevelData.LevelState.LOCK;
    public void UnlockLevel(int id) => GetLevel(id).state = LevelData.LevelState.PLAY;
    public void CompletedLevel() => GetLevel(levels.currentLevelIndex).state = LevelData.LevelState.COMPLETE;

    public LevelData GetLevel(int id)
    {
        int lvlcount = levels.levelDatas.Length;
        if (id <= lvlcount && id >= 1)
            return levels.levelDatas[id - 1];

        return null;
    }
}

[Serializable]
public class LevelManagerData
{
    public int currentLevelIndex;
    public LevelData[] levelDatas;
}

[Serializable]
public class LevelData
{
    public int index = 0;
    public LevelState state = LevelState.LOCK;
    public enum LevelState
    {
        LOCK = 0,
        PLAY = 1,
        COMPLETE = 2,
    }

    public LevelData(int index)
    {
        this.index = index;
    }
}

