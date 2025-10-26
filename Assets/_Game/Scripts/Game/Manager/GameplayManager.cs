
using System;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField]
    Player player;
    public Player Player => player;
    Level currentLevel;
    bool isGameEnd;

    void Start()
    {
        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    public void LoadLevel()
    {
        LoadGame();
        StartLevel();
    }

    public void LoadGame()
    {
        ReconstructLevel();
    }

    public void StartLevel()
    {
        player.Run();
    }

    public void ReconstructLevel()
    {
        DestructLevel();
        ConstructLevel();
    }

    public void ConstructLevel()
    {
        isGameEnd = false;
        currentLevel ??= LevelManager.Ins.LoadLevel();
        currentLevel?.OnInit();

        if (player == null)
        {
            player = SimplePool.Spawn<Player>(PoolType.PLAYER);
        }
        player?.OnInit();
        player.TF.position = currentLevel.SpawnPoint.position;
        player._OnExitLand += LevelManager.Ins.OnPlayerExitLand;
    }

    public void DestructLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        }
        if (player)
            player._OnExitLand -= LevelManager.Ins.OnPlayerExitLand;
    }

    public void Pause(bool isPause)
    {

    }

    public void OnGameEnd(bool isWin)
    {
        isGameEnd = true;
        UIManager.Ins.CloseAll();
        if (isWin)
        {
            UIManager.Ins.OpenUI<UIWin>();
            DataManager.Ins.GameData.CompletedLevel();
            DataManager.Ins.Save();
        }
        else
        {
            UIManager.Ins.OpenUI<UILose>();
        }
    }
}