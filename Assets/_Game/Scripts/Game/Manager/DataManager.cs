using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    GameData _gameData;
    public GameData GameData
    {
        get
        {
            if (_gameData == null)
            {
                Load();
                _gameData.InitData();
            }
            return _gameData;
        }
    }

    private GameData Load()
    {
        _gameData = Database.Load<GameData>();
        return _gameData;
    }
    public void Save()
    {
        Database.Save(_gameData);
    }

}