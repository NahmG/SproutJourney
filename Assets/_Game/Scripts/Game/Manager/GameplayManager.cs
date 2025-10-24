
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
        LoadGame();
        UIManager.Ins.OpenUI<UIGameplay>();

        player._OnExitLand += LevelManager.Ins.OnPlayerExitLand;

        StartLevel();
    }

    void OnDestroy()
    {
        player._OnExitLand -= LevelManager.Ins.OnPlayerExitLand;
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

        Cell spawnPoint = currentLevel.PlayerSpawnPoint;
        if (player == null)
        {
            player = SimplePool.Spawn<Player>(PoolType.PLAYER, spawnPoint.Tf.position, Quaternion.identity);
        }
        player.OnInit();
        player.SetCell(spawnPoint);
    }

    void DestructLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
        }
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
            // UIManager.Ins.OpenUI<UIWin>();
        }
        else
        {
            // UIManager.Ins.OpenUI<UILose>();
        }
    }
}