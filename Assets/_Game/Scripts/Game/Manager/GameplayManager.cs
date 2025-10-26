
using System;
using System.Collections.Generic;
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
        LevelManager.Ins.OnInit();
        UIManager.Ins.OpenUI<UIMainMenu>();
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

    void ConstructLevel()
    {
        isGameEnd = false;
        currentLevel ??= LevelManager.Ins.LoadLevel();
        currentLevel?.OnInit();

        if (player == null)
        {
            player = SimplePool.Spawn<Player>(PoolType.PLAYER, currentLevel.SpawnPoint.position, Quaternion.identity);
        }

        player?.OnInit();
        player._OnExitLand += LevelManager.Ins.OnPlayerExitLand;
    }

    void DestructLevel()
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
            LevelManager.Ins.CompletedLevel();
        }
        else
        {
            UIManager.Ins.OpenUI<UILose>();
        }
    }
}